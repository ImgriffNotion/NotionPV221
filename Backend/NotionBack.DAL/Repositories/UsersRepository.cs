using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;

namespace NotionBack.DAL.Repositories
{
    class UsersRepository(NotionDbContext context) : IUserRepository
    {
        private readonly NotionDbContext _context = context;

        public async Task Create(User item)
        {
            await _context.Users.AddAsync(item);
            //_logger.LogInformation($"User {item.Name} {item.Lastname} with ID:{item.Id} created");
        }

        public async Task Delete(Guid id)
        {
            try
            {
                User usr = await this.Get(id);
                _context.Users.Remove(usr);
            }
            catch (NullReferenceException ex)
            {
                throw new Exception(ex.Message);
            }
            //_logger.LogInformation($"User with ID: {usr.Id} deleted");
        }

        public async Task<User> Get(Guid id) =>
            await _context.Users.FindAsync(id)
            ?? throw new NullReferenceException($"User with ID: {id} not found");

        public async Task<IEnumerable<User>> GetAll() => await _context.Users.ToListAsync();

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException($"User with Email: {email} not found");
        }

        public void Update(User item)
        {
            _context.Users.Entry(item).State = EntityState.Modified;
            //_logger.LogInformation($"User with ID: {item.Id} updated");
        }
    }
}
