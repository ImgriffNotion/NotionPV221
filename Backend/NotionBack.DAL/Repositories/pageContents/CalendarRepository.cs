using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class CalendarRepository(NotionDbContext context) : ICalendarRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(Calendar item)
    {
        await _context.Calendars.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        Calendar calendar = await this.Get(id);
        calendar.DeleteDt = DateTime.Now;
        this.Update(calendar);
    }

    public async Task<Calendar> Get(Guid id)
    {
        return await _context.Calendars.FindAsync(id)
            ?? throw new NullReferenceException($"Calendar with ID: {id} not found");
    }

    public async Task<IEnumerable<Calendar>> GetAll()
    {
        return await _context.Calendars.ToListAsync();
    }

    public void Update(Calendar item)
    {
        _context.Calendars.Entry(item).State = EntityState.Modified;
    }
}
