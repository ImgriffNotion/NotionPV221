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

        public Calendar FromDTO(Calendar domain, CalendarDTO dto)
        {
            domain.Title = dto.Title;

            if (dto.InternalContent != null && dto.InternalContent.Count != 0)
            {
                var tmpBuffer = new List<CalendarContent>();
                foreach (var dtoContent in dto.InternalContent)
                {
                    var domainContent = domain.Contents.Where(obj => obj.Id == dtoContent.Id).FirstOrDefault();
                    if (domainContent != null)
                    {
                        _convertService.FromDTO(domainContent, dtoContent);
                    }
                    else
                    {
                        tmpBuffer.Add(_convertService.FromDTO(dtoContent));
                    }
                }

                foreach (var content in tmpBuffer)
                {
                    domain.Contents.Add(content);
                }
            }


            return domain;
        }

        public CalendarDTO ToDTO(Calendar model)
        {
            var calendar = new CalendarDTO()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = (Guid)model.ParentPageId,
                CreatedAt = model.CreatedAt,
                DeleteDt = model.DeleteDt,
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
