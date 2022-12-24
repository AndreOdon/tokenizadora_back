using Domain.Dto;

namespace Domain.Interfaces.Core.Application
{
    public interface IUserService
    {
        Task<UserDto?> Login(UserLoginInputDto inputDto);
    }
}