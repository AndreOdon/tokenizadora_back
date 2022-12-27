using Domain.Dto.Input;
using Domain.Dto.Result;

namespace Domain.Interfaces.Core.Application
{
    public interface IUserService
    {
        Task<UserDto?> Login(UserLoginInputDto inputDto);
    }
}