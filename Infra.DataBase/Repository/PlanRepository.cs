using Domain.Entities;
using Domain.Interfaces.Adapters.Infra.DataBase;

namespace Infra.DataBase.Repository
{
    public class PlanRepository : BaseRepository<Plan>, IPlanRepository
    {
        public PlanRepository(DatabaseContext context) : base(context)
        {
        }
    }
}