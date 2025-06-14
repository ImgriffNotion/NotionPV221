using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents;

public class ListTemplateRepository(NotionDbContext context) : IListTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(ListTemplate item)
    {
        await _context.ListTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            ListTemplate list = await this.Get(id);
            list.DeleteDt = DateTime.Now;
            this.Update(list);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ListTemplate> Get(Guid id)
    {
        return await _context.ListTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"ListTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<ListTemplate>> GetAll()
    {
        return await _context.ListTemplates.ToListAsync();
    }

    public async Task<IEnumerable<ListTemplate>> GetAll(Guid parentId)
    {
        return await _context.ListTemplates.Where(l => l.TemplateId == parentId).ToListAsync();
    }

    public void Update(ListTemplate item)
    {
        _context.ListTemplates.Entry(item).State = EntityState.Modified;
    }
}
