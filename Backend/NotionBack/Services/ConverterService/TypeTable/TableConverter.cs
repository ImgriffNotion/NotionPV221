using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeTable
{
    public class TableConverter(IConvertService<TableContentDTO, TableContent> convertService) : IConvertService<TableDTO, Table>
    {
        private readonly IConvertService<TableContentDTO, TableContent> _convertService = convertService;
        public Table FromDTO(TableDTO model)
        {
            var table = new Table()
            {
                Id = model.Id,
                Title = model.Title,
                Rows = model.Rows,
                Columns = model.Columns,
                DeleteDt = model.DeleteDt,
                ParentPageId = model.ParentPageId,
                Contents = new List<TableContent>()
            };

            if (model.InternalContent != null && model.InternalContent.Count != 0)
            {
                foreach (var content in model.InternalContent)
                {
                    table.Contents.Add(_convertService.FromDTO(content));
                }
            }

            return table;
        }

        public TableDTO ToDTO(Table model)
        {
            var table = new TableDTO()
            {
                Id = model.Id,
                Title = model.Title,
                Rows = model.Rows,
                Columns = model.Columns,
                DeleteDt = model.DeleteDt,
                ParentPageId = (Guid)model.ParentPageId,
                InternalContent = new List<TableContentDTO>()
            };

            if (model.Contents != null && model.Contents.Count != 0)
            {
                foreach (var content in model.Contents)
                {
                    table.InternalContent.Add(_convertService.ToDTO(content));
                }
            }

            return table;
        }
    }
}
