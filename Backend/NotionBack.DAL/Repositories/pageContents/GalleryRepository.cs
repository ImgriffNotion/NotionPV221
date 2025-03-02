using System;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class GalleryRepository(NotionDbContext context) : IGalleryRepository
{
    private readonly NotionDbContext _context = context;
    
    public Task Create(Gallery item)
  {
    throw new NotImplementedException();
  }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Gallery?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Gallery>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Gallery item)
    {
        throw new NotImplementedException();
    }
}
