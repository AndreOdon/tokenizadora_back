using Domain.Entities;
using Domain.Interfaces.Adapters.Infra.DataBase;

namespace Infra.DataBase.Repository
{
    public class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(DatabaseContext context) : base(context)
        {
        }
    }
}