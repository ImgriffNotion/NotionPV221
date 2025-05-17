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
                "http://10.0.1.4",
                "https://10.0.1.4",
                "http://10.0.2.4",
                "https://10.0.2.4",
                "http://13.79.53.15",
                "https://13.79.53.15",
                "http://26.211.160.167",
                "http://52.169.26.188"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    }
);
});


//"http://localhost:5000",
//"http://localhost:3000",
//"https://localhost:7114",
//"http://localhost:5157",

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(7115);

//    options.ListenAnyIP(7114, listenOptions =>
//    {
//        listenOptions.UseHttps(); 
//    });
//});

//builder.Services.AddHttpClient("IgnoreSSL").ConfigurePrimaryHttpMessageHandler(() =>
//{
//    return new HttpClientHandler
//    {
//        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
//    };
//});


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
    googleOptions.ClientId = "24881042872-ep2a4i7maue9ecm09f0viigeuvperr5t.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
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
            context.Identity.AddClaim(new Claim("urn:google:picture", picture));
        }
    };

    googleOptions.CallbackPath = "/signin-google";
});


builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConfig = builder.Configuration.GetConnectionString("Redis");

    var config = ConfigurationOptions.Parse(redisConfig);
    config.Ssl = false;
    config.AbortOnConnectFail = false;
    config.Password = "JqITgntQMmYoyAHFIQuNwSBQncbxxBQK";
    config.DefaultDatabase = 0;

    return ConnectionMultiplexer.Connect(config);
});

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

// Add services to the container.

builder.Services.AddControllers();
//string? connectionString = builder.Configuration.GetConnectionString("NotionDbConnect");
string? connectionString = builder.Configuration.GetConnectionString("LocalDbConnect");
builder.Services.AddNotionContext(connectionString!);
builder.Services.AddUnitOfWorkService();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.Services.RegistatorAllServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseSession();
app.UseAuthentication();
app.UseAuthMiddleware();
app.UseUpdateTokenMiddleware();
app.UseAuthorization();
app.MapControllers();

app.Run();
