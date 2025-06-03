using Microsoft.Extensions.DependencyInjection;
using NotionBack.Models;
using NotionBack.Models.ModelsDTO;
using NotionBack.Services.TokenService;
using System.IdentityModel.Tokens.Jwt;

namespace NotionBack.Middleware.Auth
{
    public class AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<AuthMiddleware> _logger = logger;


        public async Task Invoke(HttpContext httpContext, IServiceProvider serviceProvider, AppUserContext userContext)
        {
            var path = httpContext.Request.Path.ToString();
            Console.WriteLine($"\n\n\nAuthMiddleware {DateTime.Now.ToString()} - {path}\n {httpContext.Request.Method} \n\n\n");
            _logger.LogInformation($"AuthMiddleware {DateTime.Now.ToString()} - {path} {httpContext.Request.Method}");

            if (path.StartsWith("/imgriff/testing", StringComparison.OrdinalIgnoreCase) || path.StartsWith("/imgriff/auth", StringComparison.OrdinalIgnoreCase))
            {
                await _next(httpContext);
                return;
            }

            var encryptedToken = httpContext.Request.Cookies["token"] ?? httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(encryptedToken))
            {
                Console.WriteLine($"\n\n\nUSER-TOKEN {encryptedToken}\n\n\n");
                using var scope = serviceProvider.CreateScope();
                var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService<TokenDTO>>();

                TokenDTO token = await tokenService.CheckToken(encryptedToken);

                if (token != null)
                {
                    if (token.DeleteDt != null)
                    {
                        httpContext.Items["tokenDTO"] = token;
                        httpContext.Items["hasToBeUpdated"] = true;
                    }
                    else
                    {
                        var jwt = new JwtTokenModel() { Jwt = encryptedToken, User = token.User };
                        httpContext.Items["userId"] = token.UserId;
                        httpContext.Items["hasToBeUpdated"] = false;
                        httpContext.Response.Cookies.Append("token", jwt.Jwt, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.None,
                            Expires = DateTimeOffset.UtcNow.AddHours(TokenValidTime.VaildTimeInHours)
                        });
                    }

                    if (token.User != null)
                    {
                        userContext.userId = token.User.Id.ToString();
                        userContext.userEmail = token.User.Email ?? "";
                    }

                    await _next(httpContext);
                    return;
                }
            }

            httpContext.Response.StatusCode = 401;


            await httpContext.Response.WriteAsync($"PATH: {path} \n Middleware Unauthorized \n");
            return;
        }
    }
}
