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

        [Test]
        public void FromEntity_ReturnsValidRegionDto()
        {
            var entity = new RegionDto()
            {
                Name = "Region Test",
                Code = "001"
            }.ToEntity();

            var dto = RegionDto.FromEntity(entity);

            Assert.IsNotNull(dto);
            Assert.That(dto, Is.TypeOf<RegionDto>());
            Assert.That(dto.Name, Is.EqualTo(entity.Name));
            Assert.That(dto.Code, Is.EqualTo(entity.Code));
            Assert.That(dto.Id, Is.EqualTo(entity.Id));
        }
    }
}