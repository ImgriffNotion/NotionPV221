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
        IConvertService<TokenDTO, Token> tokenConvertService,
        IConvertService<UserDTO, User> userConvertService) : ITokenService<TokenDTO>
    {
        private int vaildHours = 3;
        private readonly String _secretKey = jwtSettings.Value.SecretKey;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<TokenDTO, Token> _tokenConvertService = tokenConvertService;
        private readonly IConvertService<UserDTO, User> _userConvertService = userConvertService;

        public async Task<TokenDTO> CheckToken(String token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var tokenId = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "TokenId")?.Value;

                if (Guid.TryParse(tokenId, out var parsedTokenId))
                {
                    try
                    {
                        var tokenFromDb = await _unitOfWork.Tokens.Get(parsedTokenId);
                        var tokenInfo = await _tokenConvertService.ToDTO(tokenFromDb);
                        tokenInfo.User = await _userConvertService.ToDTO(await _unitOfWork.Users.Get(tokenInfo.UserId));
                        if (tokenInfo != null)
                        {
                            if (tokenInfo.Exp != null)
                            {
                                if ((DateTime)tokenInfo.Exp > DateTime.UtcNow)
                                    return tokenInfo;
                                else
                                {
                                    tokenFromDb.DeleteDt = DateTime.UtcNow;
                                    await _unitOfWork.Save();

                                    tokenInfo.DeleteDt = tokenFromDb.DeleteDt;
                                    return tokenInfo;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return new TokenDTO();
                    }
                }
            }
            catch (Exception) { }
            return new TokenDTO();
        }

        public async Task<string> GenerateToken(TokenDTO tokenModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            if (tokenModel.User.Email != null)
            {
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

                await _unitOfWork.Tokens.Create(await _tokenConvertService.FromDTO(tokenModel));
                await _unitOfWork.Save();

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            return new("");
        }

    }
}
