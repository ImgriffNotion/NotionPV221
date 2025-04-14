using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;

namespace NotionBack.DAL.Repositories
{
    public class TokenRepository(NotionDbContext context) : ITokenRepository
    {
        private readonly NotionDbContext _context = context;

        public Task Create(Token item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Token> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Token>> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Token item)
        {
            throw new NotImplementedException();
        }
    }
}
