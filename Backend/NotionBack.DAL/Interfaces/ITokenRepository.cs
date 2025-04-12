using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotionBack.DAL.Models;

namespace NotionBack.DAL.Interfaces
{
    public interface ITokenRepository : IModelRepository<Token> { }
}
