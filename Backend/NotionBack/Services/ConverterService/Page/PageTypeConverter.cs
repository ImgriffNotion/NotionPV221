using NotionBack.DAL.Models;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;

namespace NotionBack.Services.ConverterService.Page
{
    public class PageTypeConverter : IConvertService<PageTypeDTO, TypePage>
    {
        public TypePage FromDTO(PageTypeDTO model)
        {
            var type = new TypePage()
            {
                Id = model.Id,
                Name = model.Name,
                TypeCode = model.TypeCode
            };

            return type;
        }
        
        public PageTypeDTO ToDTO(TypePage model)
        {
            var type = new PageTypeDTO()
            {
                Id = model.Id,
                Name = model.Name,
                TypeCode = model.TypeCode
            };

            foreach (var value in Enum.GetValues(typeof(PageType)))
            {
                if ((int)value == type.TypeCode)
                {
                    type.TypePage = value.ToString();
                    break;
                }
            }

            return type;
        }
    }
}
