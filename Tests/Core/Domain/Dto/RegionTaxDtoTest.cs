using Domain.Dto.Result;
using Domain.Entities;

namespace Tests.Core.Domain.Dto
{
    public class RegionTaxDtoTest
    {
        [Test]
        public void ToEntity_ReturnsValidRegionTaxEntity()
        {
            var dto = new RegionTaxDto()
            {
                OriginRegionId = 1,
                DestinyRegionId = 2,
                Tax = 1.9M
            };

            var entity = dto.ToEntity();

            Assert.IsNotNull(entity);
            Assert.That(entity, Is.TypeOf<RegionTax>());
            Assert.That(entity.OriginRegionId, Is.EqualTo(dto.OriginRegionId));
            Assert.That(entity.DestinyRegionId, Is.EqualTo(dto.DestinyRegionId));
            Assert.That(entity.Tax, Is.EqualTo(dto.Tax));
        }
    }
}