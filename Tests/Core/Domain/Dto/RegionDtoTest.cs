using Domain.Dto.Result;
using Domain.Entities;

namespace Tests.Core.Domain.Dto
{
    public class RegionDtoTest
    {
        [Test]
        public void ToEntity_ReturnsValidRegionEntity()
        {
            var dto = new RegionDto()
            {
                Name = "Region Test",
                Code = "001"
            };

            var entity = dto.ToEntity();

            Assert.IsNotNull(entity);
            Assert.That(entity, Is.TypeOf<Region>());
            Assert.That(entity.Name, Is.EqualTo(dto.Name));
            Assert.That(entity.Code, Is.EqualTo(dto.Code));
        }
    }
}