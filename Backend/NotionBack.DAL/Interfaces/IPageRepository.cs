using NotionBack.DAL.Models;

namespace NotionBack.DAL.Interfaces;

public interface IPageRepository : IModelRepository<Page>
{
    Task<Page> GetPageBySlug(String slug);
    Task DeletePagePermanently(Page page);
    Task<IEnumerable<Page>> GetAll(Guid userId);
}
