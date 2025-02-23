using System;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models.pageContents.pageInPageContents;

namespace NotionBack.Db.Repositories.pageContents.pageInPageContents;

public class ListContentRepository(NotionDbContext context) : IListContentReopsitory
{
    private readonly NotionDbContext _context = context;
    
    public Task Create(ListContent item)
  {
    throw new NotImplementedException();
  }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ListContent?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ListContent>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(ListContent item)
    {
        throw new NotImplementedException();
    }
}
