using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents;

public class JustPageContentTemplateRepository(NotionDbContext context)
    : IJustPageContentTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(JustPageContentTemplate item)
    {
        await _context.JustPageContentTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            JustPageContentTemplate justPageContent = await this.Get(id);
            _context.JustPageContentTemplates.Remove(justPageContent);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<JustPageContentTemplate> Get(Guid id)
    {
        return await _context.JustPageContentTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"JustPageContentTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<JustPageContentTemplate>> GetAll()
    {
        return await _context.JustPageContentTemplates.ToListAsync();
    }

    public async Task<IEnumerable<JustPageContentTemplate>> GetAll(Guid parentId)
    {
        return await _context
            .JustPageContentTemplates.Where(jpc => jpc.TemplateId == parentId)
            .ToListAsync();
    }

    public void Update(JustPageContentTemplate item)
    {
        _context.JustPageContentTemplates.Entry(item).State = EntityState.Modified;
    }
}
