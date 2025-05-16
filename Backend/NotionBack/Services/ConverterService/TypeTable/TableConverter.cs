using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeTable
{
    public class TableConverter(IConvertService<TableContentDTO, TableContent> convertService) : IConvertService<TableDTO, Table>
    {
        private readonly IConvertService<TableContentDTO, TableContent> _convertService = convertService;
        public async Task<Table> FromDTO(TableDTO model)
        {
            if (model == null)
                return new Table();

            var table = new Table()
            {
                Title = model.Title,
                Rows = model.Rows,
                Columns = model.Columns,
                Contents = new List<TableContent>()
            };

            if (model.InternalContent != null && model.InternalContent.Count != 0)
            {
                foreach (var content in model.InternalContent)
                {
                    table.Contents.Add(await _convertService.FromDTO(content));
                }
            }

            return table;
        }

        public async Task<Table> FromDTO(Table domain, TableDTO dto)
        {
            if (domain == null || dto == null)
                return domain;

            domain.Title = dto.Title;
            domain.ParentPageId = dto.ParentPageId;
            if (dto.InternalContent != null && dto.InternalContent.Count != 0)
            {
                var tmpBuffer = new List<TableContent>();
                foreach (var dtoContent in dto.InternalContent)
                {
                    if (dtoContent.Id != null)
                    {
                        var domainContent = domain.Contents.Where(obj => obj.Id == dtoContent.Id).FirstOrDefault();
                        if (domainContent != null)
                        {
                            _convertService.FromDTO(domainContent, dtoContent);
                        }
                    }
                    else
                    {
                        tmpBuffer.Add(await _convertService.FromDTO(dtoContent));
                    }
                }

                foreach (var content in tmpBuffer)
                {
                    domain.Contents.Add(content);
                }
            }


            return domain;
        }

        public async Task<TableDTO> ToDTO(Table model)
        {
            if (model == null)
                return new TableDTO();

            var table = new TableDTO()
            {
                Id = model.Id,
                Title = model.Title,
                Rows = model.Rows,
                Columns = model.Columns,
                CreatedAt = model.CreatedAt,
                DeleteDt = model.DeleteDt,
                ParentPageId = (Guid)model.ParentPageId,
                InternalContent = new List<TableContentDTO>()
            };

            if (model.Contents != null && model.Contents.Count != 0)
            {
                foreach (var content in model.Contents)
                {
                    table.InternalContent.Add(await _convertService.ToDTO(content));
                }
            }

            return table;
        }
    }
}
