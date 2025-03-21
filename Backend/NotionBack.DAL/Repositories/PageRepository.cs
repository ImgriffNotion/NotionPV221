using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;

namespace NotionBack.DAL.Repositories;

public class PageRepository(NotionDbContext context)
    : IPageRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(Page item)
    {
        await _context.Pages.AddAsync(item);
        //_logger.LogInformation($"Page {item.Title} with ID:{item.Id} created successfully");
    }

    public async Task Delete(Guid id)
    {
        Page page =
            await _context.Pages.FindAsync(id)
            ?? throw new NullReferenceException($"Page with ID {id} not found");
        page.DeleteDt = DateTime.Now;
        this.Update(page);
        //_logger.LogInformation($"Page {page.Title} with ID: {page.Id} marked as deleted");
    }

    public async Task<Page> Get(Guid id)
    {
        return await _context.Pages.FindAsync(id)
            ?? throw new NullReferenceException($"Page with ID {id} not found");
    }

    public async Task<IEnumerable<Page>> GetAll() => await _context.Pages.ToListAsync();

    public void Update(Page item)
    {
        _context.Pages.Entry(item).State = EntityState.Modified;
        //_logger.LogInformation($"Page with ID: {item.Id} updated");
    }
}
