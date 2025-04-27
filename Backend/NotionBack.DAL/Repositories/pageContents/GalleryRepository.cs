using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class GalleryRepository(NotionDbContext context) : IGalleryRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(Gallery item)
    {
        await _context.Galleries.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            Gallery gallery = await this.Get(id);
            gallery.DeleteDt = DateTime.Now;
            this.Update(gallery);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Gallery> Get(Guid id)
    {
        return await _context.Galleries.FindAsync(id)
            ?? throw new NullReferenceException($"Gallery with ID: {id} not found");
    }

    public async Task<IEnumerable<Gallery>> GetAll()
    {
        return await _context.Galleries.ToListAsync();
    }

    public void Update(Gallery item)
    {
        _context.Galleries.Entry(item).State = EntityState.Modified;
    }
}
