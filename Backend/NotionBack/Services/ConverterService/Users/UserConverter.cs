using NotionBack.DAL.Models;
using NotionBack.Models.ModelsDTO;

namespace NotionBack.Services.ConverterService.Users
{
    public class UserConverter : IConvertService<UserDTO, User>
    {
        public async Task<User> FromDTO(UserDTO model)
        {
            if (model == null)
                return new User();

            var user = new User()
            {
                Name = model.Name,
                Lastname = model.Lastname,
                Email = model.Email,
                Avatar = model.Avatar,
            };

            return user;
        }

        public async Task<User> FromDTO(User domain, UserDTO dto)
        {
            if (domain == null || dto == null)
                return domain;

            domain.Name = dto.Name;
            domain.Lastname = dto.Lastname;
            domain.Email = dto.Email;
            domain.Avatar = dto.Avatar;

            return domain;
        }

        public async Task<UserDTO> ToDTO(User model)
        {
            if (model == null)
                return new UserDTO();
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
