using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents;

public class TableTemplateRepository(NotionDbContext context) : ITableTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(TableTemplate item)
    {
        await _context.TableTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            TableTemplate table = await this.Get(id);
            table.DeleteDt = DateTime.Now;
            this.Update(table);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TableTemplate> Get(Guid id)
    {
        return await _context.TableTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"TableTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<TableTemplate>> GetAll()
    {
        return await _context.TableTemplates.ToListAsync();
    }

    public async Task<IEnumerable<TableTemplate>> GetAll(Guid parentId)
    {
        return await _context.TableTemplates.Where(t => t.TemplateId == parentId).ToListAsync();
    }

    public void Update(TableTemplate item)
    {
        _context.TableTemplates.Entry(item).State = EntityState.Modified;
    }
}
