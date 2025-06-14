using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents.pageInPageContents;

public class TableContentTemplateRepository(NotionDbContext context)
    : ITableContentTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(TableContentTemplate item)
    {
        await _context.TableContentTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            TableContentTemplate tableContent = await this.Get(id);
            _context.TableContentTemplates.Remove(tableContent);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TableContentTemplate> Get(Guid id)
    {
        return await _context.TableContentTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"TableContentTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<TableContentTemplate>> GetAll()
    {
        return await _context.TableContentTemplates.ToListAsync();
    }

    public async Task<IEnumerable<TableContentTemplate>> GetAll(Guid parentId)
    {
        return await _context
            .TableContentTemplates.Where(x => x.TableTemplateId == parentId)
            .ToListAsync();
    }

    public void Update(TableContentTemplate item)
    {
        _context.TableContentTemplates.Entry(item).State = EntityState.Modified;
    }
}
