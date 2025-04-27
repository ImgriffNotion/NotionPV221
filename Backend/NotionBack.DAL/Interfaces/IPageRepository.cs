using NotionBack.DAL.Models;

namespace NotionBack.DAL.Interfaces;

public interface IPageRepository : IModelRepository<Page>
{
    Task<Page> GetPageBySlug(String slug);
    Task<bool> DeletePagePermanently(Page page);
}
