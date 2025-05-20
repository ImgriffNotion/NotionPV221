using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeCalendar
{
    public class CalendarConverter(IConvertService<CalendarContentDTO, CalendarContent> convertService) : IConvertService<CalendarDTO, Calendar>
    {

        private readonly IConvertService<CalendarContentDTO, CalendarContent> _convertService = convertService;

        public async Task<Calendar> FromDTO(CalendarDTO model)
        {
            if (model == null)
                return new Calendar();

            var calendar = new Calendar()
            {
                Title = model.Title,
                Contents = new List<CalendarContent>()
            };

            if (model.InternalContent != null && model.InternalContent.Count != 0)
            {
                foreach (var content in model.InternalContent)
                {
                    calendar.Contents.Add(await _convertService.FromDTO(content));
                }
            }

            return calendar;
        }

        public async Task<Calendar> FromDTO(Calendar domain, CalendarDTO dto)
        {
            if (domain == null || dto == null)
                return domain;

            domain.Title = dto.Title;

            if (dto.InternalContent != null && dto.InternalContent.Count != 0)
            {
                var tmpBuffer = new List<CalendarContent>();
                foreach (var dtoContent in dto.InternalContent)
                {
                    var domainContent = domain.Contents.Where(obj => obj.Id == dtoContent.Id).FirstOrDefault();
                    if (domainContent != null)
                    {
                        await _convertService.FromDTO(domainContent, dtoContent);
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

        public async Task<CalendarDTO> ToDTO(Calendar model)
        {
            if (model == null)
                return new CalendarDTO();

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
                    calendar.InternalContent.Add(await _convertService.ToDTO(content));
                }
            }

            return calendar;
        }
    }
}
