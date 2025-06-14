using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface IBoardTemplateRepository : IModelRepository<BoardTemplate>
{
    Task<IEnumerable<BoardTemplate>> GetAll(Guid parentId);
}
