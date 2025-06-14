using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents.pageInPageContents;

public class GalleryContentTemplateRepository(NotionDbContext context) : IGalleryContentTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(GalleryContentTemplate item)
    {
        await _context.GalleryContentTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            GalleryContentTemplate galleryContent = await this.Get(id);
            _context.GalleryContentTemplates.Remove(galleryContent);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GalleryContentTemplate> Get(Guid id)
    {
        return await _context.GalleryContentTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"GalleryContentTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<GalleryContentTemplate>> GetAll()
    {
        return await _context.GalleryContentTemplates.ToListAsync();
    }

    public async Task<IEnumerable<GalleryContentTemplate>> GetAll(Guid parentId)
    {
        return await _context.GalleryContentTemplates.Where(x => x.GalleryTemplateId == parentId).ToListAsync();
    }

    public void Update(GalleryContentTemplate item)
    {
        _context.GalleryContentTemplates.Entry(item).State = EntityState.Modified;
    }
}
