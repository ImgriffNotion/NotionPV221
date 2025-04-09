using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeTable
{
    public class TableContentConverter : IConvertService<TableContentDTO, TableContent>
    {
        public TableContent FromDTO(TableContentDTO model)
        {
            var tableContent = new TableContent()
            {
                Id = model.Id,
                Row = model.Row,
                Column = model.Col,
                Data = model.Data,
                Foreground = model.Foreground,
                Background = model.Background,
                TableId = model.TableId
            };
            return tableContent;
        }

        public TableContentDTO ToDTO(TableContent model)
        {
            var tableContent = new TableContentDTO()
            {
                Id = model.Id,
                Row = model.Row,
                Col = model.Column,
                Data = model.Data,
                Foreground = model.Foreground,
                Background = model.Background,
                TableId = (Guid)model.TableId
            };
            return tableContent;
        }
    }
}
