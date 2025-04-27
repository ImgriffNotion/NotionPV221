using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotionBack.DAL.Interfaces;
using File = NotionBack.DAL.Models.fileStructure.File;

namespace NotionBack.DAL.Repositories;

public class FileRepository(NotionDbContext context) : IFileRepository
{
    private readonly NotionDbContext _context = context;

    public async Task Create(File item)
    {
        await _context.Files.AddAsync(item);
        //_logger.LogInformation($"Added file {item.Name}");
    }

    public async Task Delete(Guid id)
    {
        try
        {
            File file = await this.Get(id);
            _context.Files.Remove(file);
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);
        }

        //_logger.LogInformation($"File {file.Name} with ID: {file.Id} deleted");
    }

    public async Task<File> Get(Guid id) =>
        await _context.Files.FindAsync(id)
        ?? throw new NullReferenceException($"File with ID: {id} not found");

    public async Task<IEnumerable<File>> GetAll() => await _context.Files.ToListAsync();

    public void Update(File item)
    {
        _context.Files.Entry(item).State = EntityState.Modified;
        // _logger.LogInformation($"File with ID: {item.Id} updated");
    }
}
