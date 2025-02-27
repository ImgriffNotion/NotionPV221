using System;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models;

namespace NotionBack.Db.Repositories;

public class TypePageRepository(NotionDbContext context) : ITypePageRepository
{
  private readonly NotionDbContext _context = context;
    public Task Create(TypePage item)
  {
    throw new NotImplementedException();
  }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TypePage?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TypePage>> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(TypePage item)
    {
        throw new NotImplementedException();
    }
}
