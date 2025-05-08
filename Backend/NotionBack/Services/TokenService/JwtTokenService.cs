using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;
using NotionBack.Models;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.Settings;
using NotionBack.Services.ConverterService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotionBack.Services.TokenService
{
    public class JwtTokenService(IOptions<JwtSettings> jwtSettings,
        IUnitOfWork unitOfWork,
        IConvertService<TokenDTO, Token> tokenConvertService) : ITokenService<TokenDTO>
    {
        private int vaildHours = 3;
        private readonly String _secretKey = jwtSettings.Value.SecretKey;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<TokenDTO, Token> _tokenConvertService = tokenConvertService;

        public async Task<TokenDTO> CheckToken(String token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var tokenId = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "TokenId")?.Value;

            if (Guid.TryParse(tokenId, out var parsedTokenId))
            {
                try
                {
                    var tokenInfo = _tokenConvertService.ToDTO(await _unitOfWork.Tokens.Get(parsedTokenId));
                    if (tokenInfo != null && tokenInfo.Exp > DateTime.UtcNow)
                        return tokenInfo;

                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }

        public string GenerateToken(TokenDTO tokenModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("TokenId", tokenModel.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, tokenModel.User.Id.ToString()),
                    new Claim(ClaimTypes.Email, tokenModel.User.Email)
            }),
                Expires = DateTime.UtcNow.AddHours(vaildHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
