using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Interfaces.Templates;
using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.Templates.pageContents;

namespace NotionBack.DAL.Repositories.Templates.pageContents;

public class BoardTemplateRepository(NotionDbContext context) : IBoardTemplateRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(BoardTemplate item)
    {
        await _context.BoardTemplates.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        try
        {
            BoardTemplate board = await this.Get(id);
            board.DeleteDt = DateTime.Now;
            this.Update(board);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<BoardTemplate> Get(Guid id)
    {
        return await _context.BoardTemplates.FindAsync(id)
            ?? throw new NullReferenceException($"BoardTemplate with ID: {id} not found");
    }

    public async Task<IEnumerable<BoardTemplate>> GetAll()
    {
        return await _context.BoardTemplates.ToListAsync();
    }

    public async Task<IEnumerable<BoardTemplate>> GetAll(Guid parentId)
    {
        return await _context.BoardTemplates.Where(x => x.TemplateId == parentId).ToListAsync();
    }

    public void Update(BoardTemplate item)
    {
        _context.BoardTemplates.Entry(item).State = EntityState.Modified;
    }
}
