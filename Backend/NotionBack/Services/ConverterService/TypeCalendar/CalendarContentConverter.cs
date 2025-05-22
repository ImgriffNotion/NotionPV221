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
                    var listFile = new CalendarFile()
                    {
                        CalendarContent = calendarContent,
                        File = await _convertService.FromDTO(file),
                    };
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
