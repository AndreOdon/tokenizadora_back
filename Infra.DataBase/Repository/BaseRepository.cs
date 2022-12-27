using Domain.Entities;
using Domain.Interfaces.Adapters.Infra.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataBase.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected DatabaseContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}