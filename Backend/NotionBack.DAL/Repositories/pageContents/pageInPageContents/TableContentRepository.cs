using System;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories.pageContents.pageInPageContents;

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
