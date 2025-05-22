using NotionBack.DAL.Models;
using NotionBack.Models.ModelsDTO;

namespace NotionBack.Services.ConverterService.Token
{
    public class TokenConverter : IConvertService<TokenDTO, NotionBack.DAL.Models.Token>
    {
        public async Task<DAL.Models.Token> FromDTO(TokenDTO model)
        {
            if (model == null)
                return new DAL.Models.Token();

            var token = new DAL.Models.Token()
            {
                Id = model.Id,
                UserId = model.UserId,
                Exp = model.Exp,
                Iat = model.Iat
            };

            return token;
        }

        public async Task<DAL.Models.Token> FromDTO(DAL.Models.Token domain, TokenDTO dto)
        {
            if (domain == null || dto == null)
                return new DAL.Models.Token();

            domain.Id = dto.Id;
            domain.UserId = dto.UserId;
            domain.Exp = dto.Exp;
            domain.Iat = dto.Iat;
            
            return domain;
        }

        public async Task<TokenDTO> ToDTO(DAL.Models.Token model)
        {
            if (model == null)
                return new TokenDTO();

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
