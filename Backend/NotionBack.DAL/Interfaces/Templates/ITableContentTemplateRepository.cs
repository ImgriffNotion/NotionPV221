using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface ITableContentTemplateRepository : IModelRepository<TableContentTemplate>
{
    Task<IEnumerable<TableContentTemplate>> GetAll(Guid parentId);
}
