using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface ICalendarTemplateRepository : IModelRepository<CalendarTemplate>
{
    Task<IEnumerable<CalendarTemplate>> GetAll(Guid parentId);
}
