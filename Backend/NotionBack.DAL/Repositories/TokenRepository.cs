using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;

namespace NotionBack.DAL.Repositories
{
    public class TokenRepository(NotionDbContext context) : ITokenRepository
    {
        private readonly NotionDbContext _context = context;

        public async Task Create(Token item)
        {
            await _context.Tokens.AddAsync(item);
        }

        public async Task Delete(Guid id)
        {
            try
            {
                Token token = await this.Get(id);
                token.DeleteDt = DateTime.Now;
                this.Update(token);
            }
            catch (NullReferenceException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Token> Get(Guid id)
        {
            return await _context.Tokens.FindAsync(id)
                ?? throw new NullReferenceException($"Token {id} not found");
        }

        public async Task<IEnumerable<Token>> GetAll()
        {
            return await _context.Tokens.ToListAsync();
        }

        public void Update(Token item)
        {
            _context.Tokens.Entry(item).State = EntityState.Modified;
        }
    }
}
