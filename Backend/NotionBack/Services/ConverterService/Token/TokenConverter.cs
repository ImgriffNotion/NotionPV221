using NotionBack.DAL.Models;
using NotionBack.Models.ModelsDTO;

namespace NotionBack.Services.ConverterService.Token
{
    public class TokenConverter : IConvertService<TokenDTO, NotionBack.DAL.Models.Token>
    {
        public DAL.Models.Token FromDTO(TokenDTO model)
        {
            var token = new DAL.Models.Token()
            {
                Id = model.Id,
                UserId = model.UserId,
                Exp = model.Exp,
                Iat = model.Iat
            };

            return token;
        }

        public DAL.Models.Token FromDTO(DAL.Models.Token domain, TokenDTO dto)
        {
            domain.Id = dto.Id;
            domain.UserId = dto.UserId;
            domain.Exp = dto.Exp;
            domain.Iat = dto.Iat;
            
            return domain;
        }

        public TokenDTO ToDTO(DAL.Models.Token model)
        {
            var token = new TokenDTO()
            {
                Id = model.Id,
                UserId = model.UserId,
                Exp = model.Exp,
                Iat = model.Iat,
                DeleteDt = model.DeleteDt
            };

            return token;
        }
    }
}
