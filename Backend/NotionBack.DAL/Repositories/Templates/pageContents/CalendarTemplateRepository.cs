using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents;

public class CalendarTemplateRepository(NotionDbContext context) : ICalendarTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(CalendarTemplate item)
    {
        await _context.CalendarTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            CalendarTemplate calendar = await this.Get(id);
            calendar.DeleteDt = DateTime.Now;
            this.Update(calendar);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CalendarTemplate> Get(Guid id)
    {
        return await _context.CalendarTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"CalendarTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<CalendarTemplate>> GetAll()
    {
        return await _context.CalendarTemplates.ToListAsync();
    }

    public async Task<IEnumerable<CalendarTemplate>> GetAll(Guid parentId)
    {
        return await _context.CalendarTemplates.Where(c => c.TemplateId == parentId).ToListAsync();
    }

    public void Update(CalendarTemplate item)
    {
        _context.CalendarTemplates.Entry(item).State = EntityState.Modified;
    }
}
