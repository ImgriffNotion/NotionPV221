using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories.pageContents.pageInPageContents;

public class TableContentRepository(NotionDbContext context) : ITableContentRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(TableContent item)
    {
        await _context.TableContents.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        TableContent tableContent = await this.Get(id);
        _context.TableContents.Remove(tableContent);
    }

    public async Task<TableContent> Get(Guid id)
    {
        return await _context.TableContents.FindAsync(id)
            ?? throw new NullReferenceException($"TableContent with ID: {id} not found");
    }

    public async Task<IEnumerable<TableContent>> GetAll()
    {
        return await _context.TableContents.ToListAsync();
    }

    public void Update(TableContent item)
    {
        _context.TableContents.Entry(item).State = EntityState.Modified;
    }
}
