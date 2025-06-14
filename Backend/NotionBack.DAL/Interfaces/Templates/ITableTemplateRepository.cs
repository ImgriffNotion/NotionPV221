using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface ITableTemplateRepository : IModelRepository<TableTemplate>
{
    Task<IEnumerable<TableTemplate>> GetAll(Guid parentPageId);
}
