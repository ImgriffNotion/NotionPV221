using NotionBack.DAL.Models;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;

namespace NotionBack.Services.ConverterService.Page
{
    public class PageTypeConverter : IConvertService<PageTypeDTO, TypePage>
    {
        public async Task<TypePage> FromDTO(PageTypeDTO model)
        {
            if (model == null)
                return new TypePage();

            var type = new TypePage()
            {
                Id = model.Id,
                Name = model.Name,
                TypeCode = model.TypeCode
            };

            return type;
        }

        public async Task<TypePage> FromDTO(TypePage domain, PageTypeDTO dto)
        {
            if (domain == null || dto == null)
                return new TypePage();

            domain.Name = dto.Name;
            domain.TypeCode = dto.TypeCode;
            return domain;
        }

        public async Task<PageTypeDTO> ToDTO(TypePage model)
        {
            if (model == null)
                return new PageTypeDTO();

            var type = new PageTypeDTO()
            {
                Id = model.Id,
                Name = model.Name,
                TypeCode = model.TypeCode
            };

            return type;
        }
    }
}
