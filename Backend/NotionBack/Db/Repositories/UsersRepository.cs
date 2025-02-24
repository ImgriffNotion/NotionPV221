using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Contracts;
using NotionBack.Db.Interfaces;
using NotionBack.Db.Models;

namespace NotionBack.Db.Repositories
{
    class UsersRepository : IModelRepository<User>
    {
        private readonly NotionDbContext _context;

        public UsersRepository(NotionDbContext context)
        {
            _context = context;
        }

        public async Task Create(User item) => await _context.Users.AddAsync(item);

        public async Task Delete(Guid id)
        {
            User? usr = await _context.Users.FindAsync(id);
            if (usr != null)
                _context.Users.Remove(usr);
        }

        public async Task<User?> Get(Guid id) => await _context.Users.FindAsync(id);

        public async Task<IEnumerable<User>> GetAll() => await _context.Users.ToListAsync();

        public void Update(User item) => _context.Users.Entry(item).State = EntityState.Modified;
    }
}
