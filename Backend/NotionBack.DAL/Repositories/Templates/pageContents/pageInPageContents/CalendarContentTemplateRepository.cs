using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents.pageInPageContents;

public class CalendarContentTemplateRepository(NotionDbContext context)
    : ICalendarContentTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(CalendarContentTemplate item)
    {
        await _context.CalendarContentTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            CalendarContentTemplate calendarContent = await this.Get(id);
            _context.CalendarContentTemplates.Remove(calendarContent);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CalendarContentTemplate> Get(Guid id)
    {
        return await _context.CalendarContentTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"CalendarContentTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<CalendarContentTemplate>> GetAll()
    {
        return await _context.CalendarContentTemplates.ToListAsync();
    }

    public async Task<IEnumerable<CalendarContentTemplate>> GetAll(Guid parentId)
    {
        return await _context
            .CalendarContentTemplates.Where(x => x.CalendarTemplateId == parentId)
            .ToListAsync();
    }

    public void Update(CalendarContentTemplate item)
    {
        _context.CalendarContentTemplates.Entry(item).State = EntityState.Modified;
    }
}
