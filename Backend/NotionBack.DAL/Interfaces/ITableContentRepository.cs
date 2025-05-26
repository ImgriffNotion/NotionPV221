
using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Interfaces;

public interface ITableContentRepository : IModelRepository<TableContent>
{
    Task<IEnumerable<TableContent>> GetAll(Guid parentId);
 }
