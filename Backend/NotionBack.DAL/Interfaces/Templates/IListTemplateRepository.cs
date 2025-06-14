using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface IListTemplateRepository : IModelRepository<ListTemplate>
{
    Task<IEnumerable<ListTemplate>> GetAll(Guid parentId);
}
