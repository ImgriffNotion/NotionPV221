using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories.pageContents.pageInPageContents;

public class GalleryContentRepository(NotionDbContext context) : IGalleryContentRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(GalleryContent item)
    {
        await _context.GalleryContents.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            GalleryContent galleryContent = await this.Get(id);
            _context.GalleryContents.Remove(galleryContent);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GalleryContent> Get(Guid id)
    {
        return await _context.GalleryContents.FindAsync(id)
            ?? throw new NullReferenceException($"GalleryContent with ID: {id} not found");
    }

    public async Task<IEnumerable<GalleryContent>> GetAll()
    {
        return await _context.GalleryContents.ToListAsync();
    }

    public void Update(GalleryContent item)
    {
        _context.GalleryContents.Entry(item).State = EntityState.Modified;
    }
}
