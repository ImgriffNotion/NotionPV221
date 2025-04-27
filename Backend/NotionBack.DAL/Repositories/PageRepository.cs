using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;

namespace NotionBack.DAL.Repositories;

public class PageRepository(NotionDbContext context) : IPageRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(Page item)
    {
        await _context.Pages.AddAsync(item);
        //_logger.LogInformation($"Page {item.Title} with ID:{item.Id} created successfully");
    }

    public async Task Delete(Guid id)
    {
        Page page;
        try
        {
            page = await this.Get(id);
            page.DeleteDt = DateTime.Now;
            this.Update(page);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
        //_logger.LogInformation($"Page {page.Title} with ID: {page.Id} marked as deleted");
    }

    public async Task DeletePagePermanently(Page page)
    {
        try
        {
            Page pg = await this.Get(page.Id);
            _context.Pages.Remove(page);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Page> Get(Guid id)
    {
        return await _context.Pages.FindAsync(id)
            ?? throw new NullReferenceException($"Page with ID {id} not found");
    }

    public async Task<IEnumerable<Page>> GetAll() => await _context.Pages.ToListAsync();

    public async Task<Page> GetPageBySlug(string slug)
    {
        return await _context.Pages.Where(x => x.Slug == slug).FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException($"Page with Slug: {slug} not found");
    }

    public void Update(Page item)
    {
        _context.Pages.Entry(item).State = EntityState.Modified;
        //_logger.LogInformation($"Page with ID: {item.Id} updated");
    }
}
