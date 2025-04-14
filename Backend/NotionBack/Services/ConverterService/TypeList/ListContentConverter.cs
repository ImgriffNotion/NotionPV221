using NotionBack.DAL.Models.fileStructure;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeList
{
    public class ListContentConverter(IConvertService<FileDTO, DAL.Models.fileStructure.File> convertService) : IConvertService<ListContentDTO, ListContent>
    {
        private readonly IConvertService<FileDTO, DAL.Models.fileStructure.File> _convertService = convertService;

        public ListContent FromDTO(ListContentDTO model)
        {
            var listContent = new ListContent()
            {
                Id = model.Id,
                Title = model.Title,
                Number = model.Number,
                Date = model.Date,
                Description = model.Description,
                Index = model.Index,
                ListId = model.ListId,
                Color = model.Color,
                Files = new List<DAL.Models.fileStructure.ListFile>()
            };

            if (model.Files.Count != 0 && model.Files != null) 
            {
                foreach (var file in model.Files)
                {
                    var listFile = new ListFile()
                    {
                        ListContent = listContent,
                        File = _convertService.FromDTO(file)
                    };
                }
            }

            return listContent;
        }

        public ListContentDTO ToDTO(ListContent model)
        {
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

            if (model.Files.Count != 0 && model.Files != null)
            {
                foreach (var file in model.Files)
                {
                    listContent.Files.Add(_convertService.ToDTO(file.File));
                }
            }

            return listContent;
        }
    }
}
