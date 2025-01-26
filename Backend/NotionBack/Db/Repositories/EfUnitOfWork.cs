using NotionBack.Db.Interfaces;
using NotionBack.Db.Models;

namespace NotionBack.Db.Repositories
{
    public class EfUnitOfWork(NotionDbContext context) : IUnitOfWork
    {
        private readonly NotionDbContext context = context;

        private UsersRepository? _usersRepository;

        public IModelRepository<User> Users => _usersRepository ??= new UsersRepository(context);

        public async Task Save() => await context.SaveChangesAsync();

    }
}
