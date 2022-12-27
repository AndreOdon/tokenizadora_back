using Domain.Entities;

namespace Domain.Dto.Result
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }

        public static UserDto FromEntity(User entity, string token)
        {
            return new UserDto()
            {
                UserName = entity.UserName,
                Name = entity.Name,
                Id = entity.Id,
                Token = token,
            };
        }

        public User ToEntity()
        {
            return new User(this);
        }
    }
}