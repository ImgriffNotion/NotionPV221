using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.Templates;

namespace NotionBack.DAL.Repositories.Templates;

public class TypePageTemplateRepository(NotionDbContext context) : ITypePageTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(TypePageTemplate item)
    {
        await _context.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            TypePageTemplate item = await this.Get(id);
            _context.TypePageTemplates.Remove(item);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TypePageTemplate> Get(Guid id) =>
        await _context.TypePageTemplates.FindAsync(id)
        ?? throw new NullReferenceException($"TypePageTemplate type with ID: {id} not found");

    public async Task<IEnumerable<TypePageTemplate>> GetAll() =>
        await _context.TypePageTemplates.ToListAsync();

    public async Task<TypePageTemplate> GetTypePageTemplateByCode(int code)
    {
        return await _context.TypePageTemplates.Where(x => x.TypeCode == code).FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException($"TypePageTemplate with Code: {code} not found");
    }

    public async Task<TypePageTemplate> GetTypePageTemplateByName(string name)
    {
        return await _context.TypePageTemplates.Where(x => x.Name == name).FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException($"TypePageTemplate with Name: {name} not found");
    }

    public void Update(TypePageTemplate item)
    {
        _context.TypePageTemplates.Entry(item).State = EntityState.Modified;
    }
}
