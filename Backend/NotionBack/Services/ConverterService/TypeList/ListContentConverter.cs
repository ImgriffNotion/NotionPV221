using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.fileStructure;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeList
{
    public class ListContentConverter(IConvertService<FileDTO, DAL.Models.fileStructure.File> convertService, IUnitOfWork unitOfWork) : IConvertService<ListContentDTO, ListContent>
    {
        private readonly IConvertService<FileDTO, DAL.Models.fileStructure.File> _convertService = convertService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
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
                    listContent.Files.Add(
                        new ListFile()
                        {
                            FileId = file.Id,
                            File = await _unitOfWork.Files.Get(file.Id),
                            ListContent = listContent
                        });
                }

            }

            return listContent;
        }

        public async Task<ListContent> FromDTO(ListContent domain, ListContentDTO dto)
        {
            if (domain == null || dto == null)
                return new ListContent();

            domain.Title = dto.Title;
            domain.Number = dto.Number;
            domain.Date = dto.Date;
            domain.Description = dto.Description;
            domain.Index = dto.Index;
            domain.Color = dto.Color;

            if (domain.Files == null && dto.Files.Count != 0)
                domain.Files = new List<ListFile>();

            if (domain.Files != null)
            {
                var existingCalendarFiles = domain.Files.ToList();

                var dtoFileIds = dto.Files?.Select(f => f.Id).ToHashSet() ?? new HashSet<Guid>();
                var existingFileIds = existingCalendarFiles.Select(cf => cf.FileId).ToHashSet();

                var filesToDelete = existingCalendarFiles
                    .Where(cf => !dtoFileIds.Contains(cf.FileId))
                    .ToList();

                foreach (var fileToDelete in filesToDelete)
                {
                    domain.Files.Remove(fileToDelete);
                }

                var filesToAdd = dto.Files?
                    .Where(dtoFile => !existingFileIds.Contains(dtoFile.Id))
                    .ToList() ?? new List<FileDTO>();

                foreach (var fileToAdd in filesToAdd)
                {
                    var file = await _unitOfWork.Files.Get(fileToAdd.Id);
                    if (file != null)
                    {
                        ListFile newCalendarFile = new ListFile
                        {
                            FileId = file.Id,
                            ListContentId = domain.Id,
                            File = file,
                            ListContent = domain,
                        };
                        domain.Files.Add(newCalendarFile);

                    }
                }
            }

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
                ListId = model.ListId,
                Files = new List<FileDTO>()
            };

            if (model.Files != null && model.Files.Count != 0)
            {
                foreach (var listFile in model.Files)
                {
                    var file = await _unitOfWork.Files.Get(listFile.FileId);
                    listContent.Files.Add(await _convertService.ToDTO(file));
                }
            }

            return listContent;
        }
    }
}
