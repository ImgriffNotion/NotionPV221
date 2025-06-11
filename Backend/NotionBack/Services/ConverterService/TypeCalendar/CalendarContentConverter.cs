using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.fileStructure;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeCalendar
{
    public class CalendarContentConverter(
        IConvertService<FileDTO, DAL.Models.fileStructure.File> convertService,
        IUnitOfWork unitOfWork
    ) : IConvertService<CalendarContentDTO, CalendarContent>
    {
        private readonly IConvertService<FileDTO, DAL.Models.fileStructure.File> _convertService = convertService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CalendarContent> FromDTO(CalendarContentDTO model)
        {
            if (model == null)
                return new CalendarContent();

            var calendarContent = new CalendarContent()
            {
                Title = model.Title,
                Description = model.Description,
                PlanedDate = model.PlanedDate,
                Number = model.Number,
                Color = model.Color,
                Files = new List<CalendarFile>()
            };

            if (model.Files != null && model.Files.Count != 0)
            {
                foreach (var file in model.Files)
                {
                    calendarContent.Files.Add(
                        new CalendarFile()
                        {
                            FileId = file.Id,
                            File = await _unitOfWork.Files.Get(file.Id),
                            CalendarContent = calendarContent
                            
                        });
                }

            }

            return calendarContent;
        }

        public async Task<CalendarContent> FromDTO(CalendarContent domain, CalendarContentDTO dto)
        {
            if (domain == null || dto == null)
                return new CalendarContent();

            domain.Title = dto.Title;
            domain.Description = dto.Description;
            domain.PlanedDate = dto.PlanedDate;
            domain.Number = dto.Number;
            domain.Color = dto.Color;

            if (domain.Files == null && dto.Files.Count != 0)
                domain.Files = new List<CalendarFile>();

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
                        CalendarFile newCalendarFile = new CalendarFile
                        {
                            FileId = file.Id,
                            CalendarContentId = domain.Id,
                            File = file,
                            CalendarContent = domain,
                        };
                        domain.Files.Add(newCalendarFile);

                    }
                }
            }

            return domain;
        }

        public async Task<CalendarContentDTO> ToDTO(CalendarContent model)
        {
            if (model == null)
                return new CalendarContentDTO();

            var calendarContent = new CalendarContentDTO()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                PlanedDate = model.PlanedDate,
                Number = model.Number,
                Color = model.Color,
                CalendarId = model.CalendarId,
                Files = new List<FileDTO>(),
            };

            if (model.Files != null && model.Files.Count != 0)
            {
                foreach (var file in model.Files)
                {
                    calendarContent.Files.Add(await _convertService.ToDTO(file.File));
                }
            }

            return calendarContent;
        }
    }
}
