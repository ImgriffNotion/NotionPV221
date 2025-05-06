using NotionBack.DAL.Models;
using NotionBack.Models.ModelsDTO;

namespace NotionBack.Services.ConverterService.Users
{
    public class UserConverter : IConvertService<UserDTO, User>
    {
        public User FromDTO(UserDTO model)
        {
            var user = new User()
            {
                Name = model.Name,
                Lastname = model.Lastname,
                Email = model.Email,
                Avatar = model.Avatar,
            };

            return user;
        }

        public User FromDTO(User domain, UserDTO dto)
        {
            domain.Name = dto.Name;
            domain.Lastname = dto.Lastname;
            domain.Email = dto.Email;
            domain.Avatar = dto.Avatar;

            return domain;
        }

        public UserDTO ToDTO(User model)
        {
            var user = new UserDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Lastname = model.Lastname,
                Email = model.Email,
                Avatar = model.Avatar
            };

            return user;
        }
    }
}
