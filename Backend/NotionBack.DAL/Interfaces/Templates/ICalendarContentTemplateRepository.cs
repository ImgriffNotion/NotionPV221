using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface ICalendarContentTemplateRepository : IModelRepository<CalendarContentTemplate>
{
    Task<IEnumerable<CalendarContentTemplate>> GetAll(Guid parentId);
}
