using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.Settings;
using NotionBack.Services.ConverterService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotionBack.Services.TokenService
{
    public class JwtTokenService(IOptions<JwtSettings> jwtSettings,
        IUnitOfWork unitOfWork) : ITokenService<TokenDTO>
    {
        private int vaildHours = 3;
        private readonly String _secretKey = jwtSettings.Value.SecretKey;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public Task<bool> CheckToken(String token)
        {
            throw new NotImplementedException();
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
