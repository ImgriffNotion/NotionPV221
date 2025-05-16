using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeEmpty
{
    public class EmptyPageConverter : IConvertService<EmptyPageContentDTO, JustPageContent>
    {
        public async Task<JustPageContent> FromDTO(EmptyPageContentDTO model)
        {
            if (model == null)
                return new JustPageContent();

            var emptyContent = new JustPageContent()
            {
                Text = model.Text,
            };

            return emptyContent;
        }

        public async Task<JustPageContent> FromDTO(JustPageContent domain, EmptyPageContentDTO dto)
        {
            if (domain == null || dto == null)
                return domain;

            domain.Text = dto.Text;

            return domain;
        }

        public async Task<EmptyPageContentDTO> ToDTO(JustPageContent model)
        {
            if (model == null)
                return new EmptyPageContentDTO();   

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
