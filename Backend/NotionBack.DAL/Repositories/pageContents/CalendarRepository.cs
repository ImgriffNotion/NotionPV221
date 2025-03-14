using System;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class CalendarRepository(NotionDbContext context) : ICalendarRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(Calendar item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Calendar?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Calendar>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Calendar item)
    {
        throw new NotImplementedException();
    }
}
