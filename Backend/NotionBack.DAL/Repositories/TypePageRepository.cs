using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;

namespace NotionBack.DAL.Repositories;

public class TypePageRepository(NotionDbContext context)
    : ITypePageRepository
{
    private readonly NotionDbContext _context = context;
    

    public async Task Create(TypePage item)
    {
        await _context.AddAsync(item);
        //_logger.LogInformation($"Page Type {item.Name} created successfully");
    }

    public async Task Delete(Guid id)
    {
        TypePage item =
            await _context.TypePages.FindAsync(id)
            ?? throw new NullReferenceException($"Page type with ID: {id} not found");
        _context.TypePages.Remove(item);
        //_logger.LogInformation($"Page Type with ID: {id} deleted");
    }

    public async Task<TypePage> Get(Guid id) =>
        await _context.TypePages.FindAsync(id)
        ?? throw new NullReferenceException($"Page type with ID: {id} not found");

    public async Task<IEnumerable<TypePage>> GetAll() => await _context.TypePages.ToListAsync();

    public void Update(TypePage item)
    {
        _context.TypePages.Entry(item).State = EntityState.Modified;
        //_logger.LogInformation($"Page Type with ID:{item.Id} updated");
    }
}
