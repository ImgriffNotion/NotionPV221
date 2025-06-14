using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents;

public class GalleryTemplateRepository(NotionDbContext context) : IGalleryTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(GalleryTemplate item)
    {
        await _context.GalleryTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            GalleryTemplate gallery = await this.Get(id);
            gallery.DeleteDt = DateTime.Now;
            this.Update(gallery);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GalleryTemplate> Get(Guid id)
    {
        return await _context.GalleryTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"GalleryTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<GalleryTemplate>> GetAll()
    {
        return await _context.GalleryTemplates.ToListAsync();
    }

    public async Task<IEnumerable<GalleryTemplate>> GetAll(Guid parentId)
    {
        return await _context.GalleryTemplates.Where(g => g.TemplateId == parentId).ToListAsync();
    }

    public void Update(GalleryTemplate item)
    {
        _context.GalleryTemplates.Entry(item).State = EntityState.Modified;
    }
}
