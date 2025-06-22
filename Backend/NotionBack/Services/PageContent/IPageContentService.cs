using NotionBack.DAL.Models;

namespace NotionBack.Services.PageContent
{
    public interface IPageContentService
    {
        public Task<Page> GetContent(Page page);
    }
}
