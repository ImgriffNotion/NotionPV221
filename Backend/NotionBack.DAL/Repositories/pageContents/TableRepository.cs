using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class TableRepository(NotionDbContext context) : ITableRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(Table item)
    {
        await _context.Tables.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        Table table = await this.Get(id);
        table.DeleteDt = DateTime.Now;
        this.Update(table);
    }

    public async Task<Table> Get(Guid id)
    {
        return await _context.Tables.FindAsync(id)
            ?? throw new NullReferenceException($"Table with ID: {id} not found");
    }

    public async Task<IEnumerable<Table>> GetAll()
    {
        return await _context.Tables.ToListAsync();
    }

    public void Update(Table item)
    {
        _context.Tables.Entry(item).State = EntityState.Modified;
    }
}
