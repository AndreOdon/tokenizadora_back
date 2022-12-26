using Domain.Entities;

namespace Domain.Interfaces.Adapters.Infra.DataBase
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task SaveChangesAsync();
        Task<T?> GetById(int id);
    }
}