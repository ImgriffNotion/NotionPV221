using System;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models.pageContents;

namespace NotionBack.Db.Repositories.pageContents;

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
