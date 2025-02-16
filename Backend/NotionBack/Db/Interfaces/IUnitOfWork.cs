using NotionBack.Db.Models;

namespace NotionBack.Db.Interfaces
{
    interface IUnitOfWork
    {
        IModelRepository<User> Users { get; }
        Task Save();
    }
}
