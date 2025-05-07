using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Interfaces;

public interface IJustPageContentRepository : IModelRepository<JustPageContent>
{
    Task<IEnumerable<JustPageContent>> GetAll(Guid parentId);
}
