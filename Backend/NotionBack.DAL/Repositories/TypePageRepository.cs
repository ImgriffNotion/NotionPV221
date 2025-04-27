using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;

namespace NotionBack.DAL.Repositories;

public class TypePageRepository(NotionDbContext context) : ITypePageRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(TypePage item)
    {
        await _context.AddAsync(item);
        //_logger.LogInformation($"Page Type {item.Name} created successfully");
    }

    public async Task Delete(Guid id)
    {
        try
        {
            TypePage item = await this.Get(id);
            _context.TypePages.Remove(item);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }

        //_logger.LogInformation($"Page Type with ID: {id} deleted");
    }

    public async Task<TypePage> Get(Guid id) =>
        await _context.TypePages.FindAsync(id)
        ?? throw new NullReferenceException($"Page type with ID: {id} not found");

    public async Task<IEnumerable<TypePage>> GetAll() => await _context.TypePages.ToListAsync();

    public async Task<TypePage> GetTypePageByCode(int code)
    {
        return await _context.TypePages.Where(x => x.TypeCode == code).FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException($"TypePage with Code: {code} not found");
    }

    public async Task<TypePage> GetTypePageByName(string name)
    {
        return await _context.TypePages.Where(x => x.Name == name).FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException($"TypePage with Name: {name} not found");
    }

    public void Update(TypePage item)
    {
        _context.TypePages.Entry(item).State = EntityState.Modified;
        //_logger.LogInformation($"Page Type with ID:{item.Id} updated");
    }
}
