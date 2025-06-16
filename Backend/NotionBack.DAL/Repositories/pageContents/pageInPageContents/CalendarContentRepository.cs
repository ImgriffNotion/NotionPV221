using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories.pageContents.pageInPageContents;

public class CalendarContentRepository(NotionDbContext context) : ICalendarContentRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(CalendarContent item)
    {
        await _context.CalendarContents.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            CalendarContent calendarContent = await this.Get(id);
            _context.CalendarContents.Remove(calendarContent);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CalendarContent> Get(Guid id)
    {
        return await _context.CalendarContents.FindAsync(id)
            ?? throw new NullReferenceException($"CalendarContent with ID: {id} not found");
    }

    public async Task<IEnumerable<CalendarContent>> GetAll()
    {
        return await _context.CalendarContents.ToListAsync();
    }

    public async Task<IEnumerable<CalendarContent>> GetAll(Guid parentId)
    {
        return await _context.CalendarContents.Where(x => x.CalendarId == parentId).Include(c => c.Files).ToListAsync();
    }

    public void Update(CalendarContent item)
    {
        _context.CalendarContents.Entry(item).State = EntityState.Modified;
    }
}
