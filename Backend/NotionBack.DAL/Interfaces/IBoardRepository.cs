using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Interfaces;

public interface IBoardRepository : IModelRepository<Board>
{
    Task<IEnumerable<Board>> GetAll(Guid parentId);
}
