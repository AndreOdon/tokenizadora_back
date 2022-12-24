using Domain.Dto;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        [ExcludeFromCodeCoverage]
        public User()
        { }

        public User(UserDto userDto)
        {
            Name = userDto.Name;
            UserName = userDto.UserName;
            Password = userDto.Password;
        }

        public string Name { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}