using System;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models.pageContents;

namespace NotionBack.Db.Repositories.pageContents;

public class TableRepository(NotionDbContext context) : ITableRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(Table item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Table?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Table>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Table item)
    {
        throw new NotImplementedException();
    }
}
