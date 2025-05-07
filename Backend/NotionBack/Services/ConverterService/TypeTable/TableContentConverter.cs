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
                Background = model.Background
            };
            return tableContent;
        }

        public TableContent FromDTO(TableContent domain, TableContentDTO dto)
        {
            domain.Row = dto.Row;
            domain.Column = dto.Col;
            domain.Data = dto.Data;
            domain.Foreground = dto.Foreground;
            domain.Background = dto.Background;
            
            return domain;
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
