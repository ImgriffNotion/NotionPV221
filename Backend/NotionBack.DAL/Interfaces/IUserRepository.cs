using NotionBack.DAL.Models;

namespace NotionBack.DAL.Interfaces
{
    public interface IUserRepository : IModelRepository<User>
    {
        Task<User> GetUserByEmail(String email);
     }
}
