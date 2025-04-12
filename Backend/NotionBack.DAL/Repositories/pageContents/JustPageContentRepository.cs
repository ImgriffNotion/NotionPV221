using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class JustPageContentRepository(NotionDbContext context) : IJustPageContentRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(JustPageContent item)
    {
        await _context.JustPageContents.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        JustPageContent justPageContent =
            await _context.JustPageContents.FindAsync(id)
            ?? throw new NullReferenceException($"JustPageContent with ID: {id} not found");
        _context.JustPageContents.Remove(justPageContent);
    }

    public async Task<JustPageContent> Get(Guid id)
    {
        return await _context.JustPageContents.FindAsync(id)
            ?? throw new NullReferenceException($"JustPageContent with ID: {id} not found");
    }

    public async Task<IEnumerable<JustPageContent>> GetAll()
    {
        return await _context.JustPageContents.ToListAsync();
    }

    public void Update(JustPageContent item)
    {
        _context.JustPageContents.Entry(item).State = EntityState.Modified;
    }
}
