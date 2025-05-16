using NotionBack.DAL.Models.fileStructure;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeList
{
    public class ListContentConverter(IConvertService<FileDTO, DAL.Models.fileStructure.File> convertService) : IConvertService<ListContentDTO, ListContent>
    {
        private readonly IConvertService<FileDTO, DAL.Models.fileStructure.File> _convertService = convertService;

        public async Task<ListContent> FromDTO(ListContentDTO model)
        {
            if (model == null)
                return new ListContent();

            var listContent = new ListContent()
            {
                Title = model.Title,
                Number = model.Number,
                Date = model.Date,
                Description = model.Description,
                Index = model.Index,
                Color = model.Color,
                Files = new List<DAL.Models.fileStructure.ListFile>()
            };

            if (model.Files != null && model.Files.Count != 0)
            {
                foreach (var file in model.Files)
                {
                    var listFile = new ListFile()
                    {
                        ListContent = listContent,
                        File = await _convertService.FromDTO(file)
                    };
                }
            }

            return listContent;
        }

        public async Task<ListContent> FromDTO(ListContent domain, ListContentDTO dto)
        {
            if (domain == null || dto == null)
                return domain;

            domain.Title = dto.Title;
            domain.Number = dto.Number;
            domain.Date = dto.Date;
            domain.Description = dto.Description;
            domain.Index = dto.Index;
            domain.Color = dto.Color;

            return domain;
        }

        public async Task<ListContentDTO> ToDTO(ListContent model)
        {
            if (model == null)
                return new ListContentDTO();

            var listContent = new ListContentDTO()
            {
                Id = model.Id,
                Title = model.Title,
                Number = model.Number,
                Date = model.Date,
                Description = model.Description,
                Color = model.Color,
                Index = model.Index,
                ListId = (Guid)model.ListId,
                Files = new List<FileDTO>()
            };

            if (model.Files != null && model.Files.Count != 0)
            {
                foreach (var file in model.Files)
                {
                    listContent.Files.Add(await _convertService.ToDTO(file.File));
                }
            }

            return listContent;
        }
    }
}
