using System;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Repositories.pageContents;

public class BoardRepository(NotionDbContext context) : IBoardRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(Board item)
    {
        await _context.Boards.AddAsync(item);
    }

    public async Task Delete(Guid id)
    {
        Board board =
            await _context.Boards.FindAsync(id)
            ?? throw new NullReferenceException($"Board with ID: {id} not found");
        board.DeleteDt = DateTime.Now;
        this.Update(board);
    }

    public async Task<Board> Get(Guid id)
    {
        return await _context.Boards.FindAsync(id)
            ?? throw new NullReferenceException($"Board with ID: {id} not found");
    }

    public async Task<IEnumerable<Board>> GetAll()
    {
        return await _context.Boards.ToListAsync();
    }

    public void Update(Board item)
    {
        _context.Boards.Entry(item).State = EntityState.Modified;
    }
}
