using System;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models.pageContents;

namespace NotionBack.Db.Repositories.pageContents;

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
