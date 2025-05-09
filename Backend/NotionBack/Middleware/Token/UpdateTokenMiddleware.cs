using NotionBack.DAL.Models;
using NotionBack.Models;
using NotionBack.Models.ModelsDTO;
using NotionBack.Services.TokenService;

namespace NotionBack.Middleware.Token
{
    public class UpdateTokenMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        public async Task Invoke(HttpContext httpContext, ITokenService<TokenDTO> tokenService)
        {
            var path = httpContext.Request.Path.ToString();
            if (path.StartsWith("/imgriff/auth", StringComparison.OrdinalIgnoreCase))
            {
                await _next(httpContext);
                return;
            }


            try
            {
                var hasToBeUpdated = (bool)httpContext.Items["hasToBeUpdated"];
                if (hasToBeUpdated)
                {
                    var token = (TokenDTO)httpContext.Items["tokenDTO"];
                    if (token != null)
                    {
                        var tokenDto = new TokenDTO()
                        {
                            Id = Guid.NewGuid(),
                            UserId = token.UserId,
                            Iat = DateTime.UtcNow,
                            Exp = DateTime.UtcNow.AddHours(TokenValidTime.VaildTimeInHours),
                            User = token.User
                        };

                        var jwt = await tokenService.GenerateToken(tokenDto);
                        var jwtTokenModel = new JwtTokenModel()
                        {
                            Jwt = jwt,
                            User = token.User
                        };

                        httpContext.Items.Remove("hasToBeUpdated");
                        httpContext.Items.Remove("tokenDTO");
                        httpContext.Items["jwt"] = jwtTokenModel;

                    }
                }
            }
            catch (Exception) { }

            await _next(httpContext);
            return;
        }
    }
}
