using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Interfaces;

public interface IGalleryContentRepository : IModelRepository<GalleryContent>
{ 
  Task<IEnumerable<GalleryContent>> GetAll(Guid parentId);
}
