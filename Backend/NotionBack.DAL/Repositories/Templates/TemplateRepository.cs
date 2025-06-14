using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates;

namespace NotionBack.DAL.Repositories.Templates;

public class TemplateRepository(NotionDbContext context) : ITemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(Template item)
    {
        await _context.Templates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        Template template;
        try
        {
            template = await this.Get(id);
            _context.Templates.Remove(template);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteTemplatePermanently(Template template)
    {
        try
        {
            Template t = await this.Get(template.Id);
            _context.Templates.Remove(t);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Template> Get(Guid id)
    {
        return await _context.Templates.FindAsync(id)
            ?? throw new NullReferenceException($"Template with ID {id} not found");
    }

    public async Task<IEnumerable<Template>> GetAll() => await _context.Templates.ToListAsync();

    public void Update(Template item)
    {
        _context.Templates.Entry(item).State = EntityState.Modified;
    }
}
