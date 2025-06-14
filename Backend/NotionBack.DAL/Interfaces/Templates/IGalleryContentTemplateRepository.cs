using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Interfaces.Templates;

public interface IGalleryContentTemplateRepository : IModelRepository<GalleryContentTemplate>
{ 
  Task<IEnumerable<GalleryContentTemplate>> GetAll(Guid parentId);
}
