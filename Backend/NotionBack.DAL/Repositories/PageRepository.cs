using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;

namespace NotionBack.DAL.Repositories;

public class PageRepository(NotionDbContext context) : IPageRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(Page item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Page?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Page>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Page item)
    {
        throw new NotImplementedException();
    }
}
