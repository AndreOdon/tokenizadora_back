using Domain.Dto.Result;
using Domain.Interfaces.Adapters.Infra.DataBase;
using Domain.Interfaces.Core.Application;

namespace Application.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<List<RegionDto>> GetAllRegions()
        {
            var regions = await _regionRepository.GetAll();

            return regions.Select(x => RegionDto.FromEntity(x)).ToList();
        }
    }
}