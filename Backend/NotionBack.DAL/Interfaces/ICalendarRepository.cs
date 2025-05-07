using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Interfaces;

public interface ICalendarRepository : IModelRepository<Calendar>
{
  Task<IEnumerable<Calendar>> GetAll(Guid parentId);
 }
