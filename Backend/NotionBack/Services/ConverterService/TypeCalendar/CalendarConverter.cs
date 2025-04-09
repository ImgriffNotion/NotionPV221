using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeCalendar
{
    public class CalendarConverter(IConvertService<CalendarContentDTO, CalendarContent> convertService) : IConvertService<CalendarDTO, Calendar>
    {

        private readonly IConvertService<CalendarContentDTO, CalendarContent> _convertService = convertService;

        public Calendar FromDTO(CalendarDTO model)
        {
            var calendar = new Calendar()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = model.ParentPageId,
                Contents = new List<CalendarContent>()
            };

            if (model.InternalContent != null && model.InternalContent.Count != 0)
            {
                foreach (var content in model.InternalContent)
                {
                    calendar.Contents.Add(_convertService.FromDTO(content));
                }
            }

            return calendar;
        }

        public CalendarDTO ToDTO(Calendar model)
        {
            var calendar = new CalendarDTO()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = (Guid)model.ParentPageId,
                InternalContent = new List<CalendarContentDTO>()
            };

            if (model.Contents != null && model.Contents.Count != 0)
            {
                foreach (var content in model.Contents)
                {
                    calendar.InternalContent.Add(_convertService.ToDTO(content));
                }
            }

            return calendar;
        }
    }
}
