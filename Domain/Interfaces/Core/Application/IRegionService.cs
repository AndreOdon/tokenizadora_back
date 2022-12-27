using Domain.Dto.Result;

namespace Domain.Interfaces.Core.Application
{
    public interface IRegionService
    {
        Task<List<RegionDto>> GetAllRegions();
    }
}