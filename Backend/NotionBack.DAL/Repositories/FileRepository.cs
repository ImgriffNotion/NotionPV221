using System;
using NotionBack.DAL.Interfaces;

namespace NotionBack.DAL.Repositories;

public class FileRepository(NotionDbContext context) : IFileRepository
{
    private readonly NotionDbContext _context = context;

    public Task Create(Models.fileStructure.File item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.fileStructure.File?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Models.fileStructure.File>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Models.fileStructure.File item)
    {
        throw new NotImplementedException();
    }
}
