using NotionBack.DAL.Models.fileStructure;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeCalendar
{
    public class CalendarContentConverter(
        IConvertService<FileDTO, DAL.Models.fileStructure.File> convertService
    ) : IConvertService<CalendarContentDTO, CalendarContent>
    {
        private readonly IConvertService<FileDTO, DAL.Models.fileStructure.File> _convertService =
            convertService;

        public CalendarContent FromDTO(CalendarContentDTO model)
        {
            var calendarContent = new CalendarContent()
            {
                Id = model.Id,
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
                    var listFile = new CalendarFile()
                    {
                        CalendarContent = calendarContent,
                        File = _convertService.FromDTO(file),
                    };
                }
            }

            return calendarContent;
        }

        public CalendarContent FromDTO(CalendarContent domain, CalendarContentDTO dto)
        {
            domain.Title = dto.Title;
            domain.Description = dto.Description;
            domain.PlanedDate = dto.PlanedDate;
            domain.Number = dto.Number;
            domain.Color = dto.Color;

            return domain;
        }

        public CalendarContentDTO ToDTO(CalendarContent model)
        {
            var calendarContent = new CalendarContentDTO()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                PlanedDate = model.PlanedDate,
                Number = model.Number,
                Color = model.Color,
                CalendarId = (Guid)model.CalendarId,
                Files = new List<FileDTO>(),
            };

            if (model.Files != null && model.Files.Count != 0)
            {
                foreach (var file in model.Files)
                {
                    calendarContent.Files.Add(_convertService.ToDTO(file.File));
                }
            }

            return calendarContent;
        }
    }
}
