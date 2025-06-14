using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface IJustPageContentTemplateRepository : IModelRepository<JustPageContentTemplate>
{
    Task<IEnumerable<JustPageContentTemplate>> GetAll(Guid parentId);
}
