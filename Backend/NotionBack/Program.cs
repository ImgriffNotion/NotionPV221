using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using NotionBack.Services;
using StackExchange.Redis;
using NotionBack.Models.Settings;
using NotionBack.Middleware.Auth;
using NotionBack.Middleware.Token;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
    "AllowFrontend",
    policy =>
    {
        policy
            .WithOrigins(
               "http://127.0.0.1:5500",
               "http://localhost:3000",
               "https://green-field-0f96be703.2.azurestaticapps.net"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    }
);
});

#region WEB host

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(7115);

//    //options.ListenAnyIP(7114, listenOptions =>
//    //{
//    //    listenOptions.UseHttps();
//    //});


//});

#endregion

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "Session";
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(googleOptions =>
{
    var googleSection = builder.Configuration.GetSection("GoogleOptions");
    string? clientId = googleSection["ClientId"];
    string? clientSecret = googleSection["ClientSecret"];

    if (clientId != null && clientSecret != null)
    {
        googleOptions.ClientId = clientId;
        googleOptions.ClientSecret = clientSecret;
        googleOptions.SaveTokens = true;
        googleOptions.Scope.Add("openid");
        googleOptions.Scope.Add("email");
        googleOptions.Scope.Add("profile");

        googleOptions.ClaimActions.MapJsonKey("picture", "picture");

        googleOptions.Events.OnCreatingTicket = async context =>
        {
            var picture = context.User.GetProperty("picture").GetString();

            if (!string.IsNullOrEmpty(picture))
            {
                context?.Identity?.AddClaim(new Claim("urn:google:picture", picture));
            }
        };

        googleOptions.CallbackPath = "/signin-google";
    }
});

// Redis for OTP
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConfig = builder.Configuration.GetConnectionString("Redis");

    var config = ConfigurationOptions.Parse(redisConfig ?? "");
    config.Ssl = false;
    config.AbortOnConnectFail = false;
    config.Password = "JqITgntQMmYoyAHFIQuNwSBQncbxxBQK";
    config.DefaultDatabase = 0;

    return ConnectionMultiplexer.Connect(config);
});

// Jwt secret key
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

// Add services to the container.

builder.Services.AddControllers();
string? connectionString = builder.Configuration.GetConnectionString("NotionDbConnect");
//connectionString = builder.Configuration.GetConnectionString("LocalDbConnect");
builder.Services.AddNotionContext(connectionString!);
builder.Services.AddUnitOfWorkService();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' [space] and then your valid token.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // For Azure Log Stream
builder.Logging.AddDebug();
// Optional: AddAzureWebAppDiagnostics for file logs
builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.AddApplicationInsightsTelemetry();

// Register of all services
builder.Services.RegistatorAllServices();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthMiddleware();
app.UseUpdateTokenMiddleware();
app.UseAuthorization();
app.MapControllers();

app.Run();
