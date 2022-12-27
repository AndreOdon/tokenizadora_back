using Application.Util;
using Domain.Dto.Input;
using Domain.Dto.Result;
using Domain.Entities;
using Domain.Interfaces.Adapters.Infra.DataBase;
using Domain.Interfaces.Core.Application;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Login(UserLoginInputDto inputDto)
        {
            var entity = await _userRepository.GetByUserName(inputDto.User);

            if (entity == default(User) || entity.Password != Security.Encrypt(inputDto.Password.ToLower()))
                return null;

            return UserDto.FromEntity(entity, Security.GenerateToken(entity));
        }
    }
}