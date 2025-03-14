using System;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

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
