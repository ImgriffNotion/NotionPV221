using System;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models.pageContents.pageInPageContents;

namespace NotionBack.Db.Repositories.pageContents.pageInPageContents;

public class TableContentRepository(NotionDbContext context) : ITableContentRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(TableContent item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TableContent?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TableContent>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(TableContent item)
    {
        throw new NotImplementedException();
    }
}
