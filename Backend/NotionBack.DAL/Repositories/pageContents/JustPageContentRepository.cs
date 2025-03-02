using System;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class JustPageContentRepository(NotionDbContext context) : IJustPageContentRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(JustPageContent item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<JustPageContent?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<JustPageContent>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(JustPageContent item)
    {
        throw new NotImplementedException();
    }
}
