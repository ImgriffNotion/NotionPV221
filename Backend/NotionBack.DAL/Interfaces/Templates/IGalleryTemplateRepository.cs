using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface IGalleryTemplateRepository : IModelRepository<GalleryTemplate>
{
    Task<IEnumerable<GalleryTemplate>> GetAll(Guid parentId);
}
