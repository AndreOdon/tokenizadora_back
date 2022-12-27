using Domain.Dto.Result;
using Domain.Entities;
using Infra.DataBase.Repository;
using Tests.Adapters.Driven.Infra.DataBase.ContextMock;

namespace Tests.Adapters.Driven.Infra.DataBase.Repository
{
    public class RegionTaxRepositoryTest : DataBaseMockTest
    {
        private RegionTaxRepository _regionTaxRepository;
        private int _originId;
        private int _destinyId;

        [SetUp]
        public async Task SetUp()
        {
            var context = GetContext();
            _regionTaxRepository = new RegionTaxRepository(context);

            var regionRepository = new RegionRepository(context);
            var originEntity = new RegionDto()
            {
                Code = "001",
                Name = "Origin",
            }.ToEntity();
            var destinyEntity = new RegionDto()
            {
                Code = "002",
                Name = "Destiny",
            }.ToEntity();

            await regionRepository.AddAsync(originEntity);
            await regionRepository.AddAsync(destinyEntity);

            var regionTaxEntity = new RegionTaxDto()
            {
                DestinyRegionId = destinyEntity.Id,
                OriginRegionId = originEntity.Id,
                Tax = 1.25M
            }.ToEntity();

            await _regionTaxRepository.AddAsync(regionTaxEntity);

            _originId = originEntity.Id;
            _destinyId = destinyEntity.Id;
        }

        [Test]
        public async Task GetByOriginDestiny_ExistindOriginDestiny_ReturnValidRegionTaxEntity()
        {
            var result = await _regionTaxRepository.GetByOriginDestiny(_originId, _destinyId);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<RegionTax>());
            Assert.That(result.OriginRegionId, Is.EqualTo(_originId));
            Assert.That(result.DestinyRegionId, Is.EqualTo(_destinyId));
            Assert.That(result.Tax, Is.EqualTo(1.25));
        }

        [Test]
        public async Task GetByOriginDestiny_ExistindOriginNotExistingDestiny_ReturnNull()
        {
            var result = await _regionTaxRepository.GetByOriginDestiny(_originId, 12);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetByOriginDestiny_NotExistindOriginExistingDestiny_ReturnNull()
        {
            var result = await _regionTaxRepository.GetByOriginDestiny(12, _destinyId);

            Assert.IsNull(result);
        }
    }
}