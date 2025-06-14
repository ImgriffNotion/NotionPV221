using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface IListContentTemplateRepository : IModelRepository<ListContentTemplate>
{
    Task<IEnumerable<ListContentTemplate>> GetAll(Guid parentId);
}
