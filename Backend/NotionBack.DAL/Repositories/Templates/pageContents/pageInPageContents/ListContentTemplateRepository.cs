using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents.pageInPageContents;

public class ListContentTemplateRepository(NotionDbContext context) : IListContentTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(ListContentTemplate item)
    {
        await _context.ListContentTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            ListContentTemplate listContent = await this.Get(id);
            _context.ListContentTemplates.Remove(listContent);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ListContentTemplate> Get(Guid id)
    {
        return await _context.ListContentTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"ListContentTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<ListContentTemplate>> GetAll()
    {
        return await _context.ListContentTemplates.ToListAsync();
    }

    public async Task<IEnumerable<ListContentTemplate>> GetAll(Guid parentId)
    {
        return await _context
            .ListContentTemplates.Where(x => x.ListTemplateId == parentId)
            .ToListAsync();
    }

    public void Update(ListContentTemplate item)
    {
        _context.ListContentTemplates.Entry(item).State = EntityState.Modified;
    }
}
