using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Interfaces;

public interface ICalendarContentRepository : IModelRepository<CalendarContent>
{
    Task<IEnumerable<CalendarContent>> GetAll(Guid parentId);
 }
