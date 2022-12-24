using Domain.Entities;

namespace Domain.Interfaces.Adapters.Infra.DataBase
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUserName(string? userName);
    }
}