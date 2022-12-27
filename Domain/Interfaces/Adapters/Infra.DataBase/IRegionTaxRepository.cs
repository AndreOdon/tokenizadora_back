using Domain.Entities;

namespace Domain.Interfaces.Adapters.Infra.DataBase
{
    public interface IRegionTaxRepository : IBaseRepository<RegionTax>
    {
        Task<RegionTax?> GetByOriginDestiny(int originId, int destinyId);
    }
}