using Domain.Entities;
using Domain.Interfaces.Adapters.Infra.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataBase.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<User?> GetByUserName(string userName)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
        }
    }
}