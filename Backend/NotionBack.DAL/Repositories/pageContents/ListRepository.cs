using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class ListRepository(NotionDbContext context) : IListRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(List item)
    {
        await _context.Lists.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            List list = await this.Get(id);
            list.DeleteDt = DateTime.Now;
            this.Update(list);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List> Get(Guid id)
    {
        return await _context.Lists.FindAsync(id)
            ?? throw new NullReferenceException($"List with ID: {id} not found");
    }

    public async Task<IEnumerable<List>> GetAll()
    {
        return await _context.Lists.ToListAsync();
    }

    public async Task<IEnumerable<List>> GetAll(Guid parentId)
    {
        return await _context.Lists.Where(l => l.ParentPageId == parentId).ToListAsync();
    }

    public void Update(List item)
    {
        _context.Lists.Entry(item).State = EntityState.Modified;
    }
}
