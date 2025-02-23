using System;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models.pageContents.pageInPageContents;

namespace NotionBack.Db.Repositories.pageContents.pageInPageContents;

public class GalleryContentRepository(NotionDbContext context) : IGalleryContentRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(GalleryContent item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<GalleryContent?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GalleryContent>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(GalleryContent item)
    {
        throw new NotImplementedException();
    }
}
