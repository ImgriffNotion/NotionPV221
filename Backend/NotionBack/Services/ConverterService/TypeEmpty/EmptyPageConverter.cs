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
                Index = model.Index,
                ParentPageId = model.ParentPageId,
                Text = model.Text
            };

            return emptyContent;
        }

        public EmptyPageContentDTO ToDTO(JustPageContent model)
        {
            var emptyContent = new EmptyPageContentDTO()
            {
                Id = model.Id,
                Index = model.Index,
                ParentPageId = (Guid)model.ParentPageId,
                Text = model.Text
            };

            return emptyContent;
        }
    }
}
