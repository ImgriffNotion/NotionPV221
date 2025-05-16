using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeTable
{
    public class TableContentConverter : IConvertService<TableContentDTO, TableContent>
    {
        public async Task<TableContent> FromDTO(TableContentDTO model)
        {
            if (model == null)
                return new TableContent();

            var tableContent = new TableContent()
            {
                Row = model.Row,
                Column = model.Col,
                Data = model.Data,
                Foreground = model.Foreground,
                Background = model.Background
            };
            return tableContent;
        }

        public async Task<TableContent> FromDTO(TableContent domain, TableContentDTO dto)
        {
            if (domain == null || dto == null)
                return domain;

            domain.Row = dto.Row;
            domain.Column = dto.Col;
            domain.Data = dto.Data;
            domain.Foreground = dto.Foreground;
            domain.Background = dto.Background;
            
            return domain;
        }

        public async Task<TableContentDTO> ToDTO(TableContent model)
        {
            if (model == null)
                return new TableContentDTO();

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
