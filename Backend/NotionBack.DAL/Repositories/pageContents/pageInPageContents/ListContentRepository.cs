using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents.pageInPageContents;

namespace NotionBack.DAL.Repositories.pageContents.pageInPageContents;

public class ListContentRepository(NotionDbContext context) : IListContentReopsitory
{
    private readonly NotionDbContext _context = context;

    public async Task Create(ListContent item)
    {
        await _context.ListContents.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            ListContent listContent = await this.Get(id);
            _context.ListContents.Remove(listContent);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ListContent> Get(Guid id)
    {
        return await _context.ListContents.FindAsync(id)
            ?? throw new NullReferenceException($"ListContent with ID: {id} not found");
    }

    public async Task<IEnumerable<ListContent>> GetAll()
    {
        return await _context.ListContents.ToListAsync();
    }

    public async Task<IEnumerable<ListContent>> GetAll(Guid parentId)
    {
        return await _context.ListContents.Where(x => x.ListId == parentId).Include(c => c.Files).ToListAsync();
    }

    public void Update(ListContent item)
    {
        _context.ListContents.Entry(item).State = EntityState.Modified;
    }
}
