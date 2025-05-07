using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Interfaces;

public interface ITableRepository : IModelRepository<Table>
{
  Task<IEnumerable<Table>> GetAll(Guid parentPageId);
 }
