using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL;
using NotionBack.DAL.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using NotionBack.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        policy => policy.AllowAnyOrigin()
//                        .AllowAnyMethod()
//                        .AllowAnyHeader());
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://127.0.0.1:5500")
                .WithOrigins("https://localhost:7114")
                .WithOrigins("http://localhost:5157")
                .WithOrigins("http://10.0.1.4")
                .WithOrigins("http://13.79.53.15/")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
    );
});

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


// Add services to the container.

builder.Services.AddControllers();
string? connectionString = builder.Configuration.GetConnectionString("NotionDbConnect");
builder.Services.AddNotionContext(connectionString!);
builder.Services.AddUnitOfWorkService();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegistatorAllServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

//app.UseCors("AllowAll");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
