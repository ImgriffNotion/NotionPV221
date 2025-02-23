using System;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models.pageContents;

namespace NotionBack.Db.Repositories.pageContents;

public class ListRepository(NotionDbContext context) : IListRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(List item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<List>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(List item)
    {
        throw new NotImplementedException();
    }
}
