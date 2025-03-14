using System;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class BoardRepository(NotionDbContext context) : IBoardRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(Board item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Board?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Board>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Board item)
    {
        throw new NotImplementedException();
    }
}
