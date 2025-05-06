using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeEmpty
{
    public class EmptyPageConverter : IConvertService<EmptyPageContentDTO, JustPageContent>
    {
        public JustPageContent FromDTO(EmptyPageContentDTO model)
        {
            var emptyContent = new JustPageContent()
            {
                Id = model.Id,
                ParentPageId = model.ParentPageId,
                Text = model.Text,
            };

            return emptyContent;
        }

        public JustPageContent FromDTO(JustPageContent domain, EmptyPageContentDTO dto)
        {
            domain.Text = dto.Text;

            return domain;
        }

        public EmptyPageContentDTO ToDTO(JustPageContent model)
        {
            var emptyContent = new EmptyPageContentDTO()
            {
                Id = model.Id,
                ParentPageId = (Guid)model.ParentPageId,
                Text = model.Text,
            };

            return emptyContent;
        }
    }
}
