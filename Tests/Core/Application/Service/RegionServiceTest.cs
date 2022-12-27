using Application.Services;
using Domain.Dto.Result;
using Domain.Entities;
using Domain.Interfaces.Adapters.Infra.DataBase;
using NSubstitute;

namespace Tests.Core.Application.Service
{
    [TestFixture]
    public class RegionServiceTest
    {
        private IRegionRepository _regionRepository;
        private RegionService _regionService;

        [SetUp]
        public void SetUp()
        {
            _regionRepository = Substitute.For<IRegionRepository>();
            _regionService = new RegionService(_regionRepository);
        }

        [Test]
        public async Task GetAll_ReturnsListValidRegionDto()
        {
            var entity1 = new RegionDto()
            {
                Name = "Region Test 1",
                Code = "001"
            }.ToEntity();
            var entity2 = new RegionDto()
            {
                Name = "Region Test 2",
                Code = "002"
            }.ToEntity();
            var entity3 = new RegionDto()
            {
                Name = "Region Test 3",
                Code = "003"
            }.ToEntity();

            var entityList = new List<Region>();
            entityList.Add(entity1);
            entityList.Add(entity2);
            entityList.Add(entity3);

            _regionRepository.GetAll().Returns(Task.FromResult<List<Region>>(entityList));

            var result = await _regionService.GetAllRegions();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<RegionDto>>());
            Assert.That(result, Has.Count.EqualTo(3));
        }
    }
}