using System;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models.pageContents.pageInPageContents;

namespace NotionBack.Db.Repositories.pageContents.pageInPageContents;

public class CalendarContentRepository(NotionDbContext context) : ICalendarContentRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(CalendarContent item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<CalendarContent?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CalendarContent>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(CalendarContent item)
    {
        throw new NotImplementedException();
    }
}
