using System;
using NotionBack.DAL.Models.pageContents;

namespace NotionBack.DAL.Interfaces;

public interface IListRepository : IModelRepository<List>
{
    Task<IEnumerable<List>> GetAll(Guid parentId);
}
