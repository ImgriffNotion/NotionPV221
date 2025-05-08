using Microsoft.Extensions.DependencyInjection;
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
                    httpContext.Items["userId"] = token.UserId;
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
