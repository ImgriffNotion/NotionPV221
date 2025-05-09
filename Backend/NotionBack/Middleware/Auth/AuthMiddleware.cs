using Microsoft.Extensions.DependencyInjection;
using NotionBack.Models;
using NotionBack.Models.ModelsDTO;
using NotionBack.Services.TokenService;
using System.IdentityModel.Tokens.Jwt;

namespace NotionBack.Middleware.Auth
{
    public class AuthMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;


        public async Task Invoke(HttpContext httpContext, IServiceProvider serviceProvider)
        {
            var path = httpContext.Request.Path.ToString();
            Console.WriteLine($"\n\n\nAuthMiddleware {DateTime.Now.ToString()} - {path}\n\n\n");


            if (path.StartsWith("/imgriff/auth", StringComparison.OrdinalIgnoreCase))
            {
                await _next(httpContext);
                return;
            }


            var encryptedToken = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(encryptedToken))
            {
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
                        httpContext.Items["jwt"] = new JwtTokenModel() { Jwt = encryptedToken};
                        httpContext.Items["userId"] = token.UserId;
                        httpContext.Items["hasToBeUpdated"] = false;
                    }

                    await _next(httpContext);
                    return;
                }
            }

            httpContext.Response.StatusCode = 401;
            await httpContext.Response.WriteAsync("Unauthorized");
            return;
        }
    }
}
