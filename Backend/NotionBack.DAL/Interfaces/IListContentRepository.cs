using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Interfaces;

public interface IListContentRepository : IModelRepository<ListContent>
{
    Task<IEnumerable<ListContent>> GetAll(Guid parentId);
}
