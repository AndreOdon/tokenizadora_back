using Domain.Entities;
using Domain.Interfaces.Adapters.Infra.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataBase.Repository
{
    public class RegionTaxRepository : BaseRepository<RegionTax>, IRegionTaxRepository
    {
        public RegionTaxRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<RegionTax?> GetByOriginDestiny(int originId, int destinyId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.OriginRegionId == originId && x.DestinyRegionId == destinyId);
        }
    }
}