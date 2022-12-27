using Domain.Dto.Result;
using Domain.Entities;

namespace Tests.Core.Domain.Dto
{
    public class PlanDtoTest
    {
        [Test]
        public void ToEntity_ReturnsValidPlanEntity()
        {
            var dto = new PlanDto()
            {
                Name = "Plan Test",
                Minutes = 10
            };

            var entity = dto.ToEntity();

            Assert.IsNotNull(entity);
            Assert.That(entity, Is.TypeOf<Plan>());
            Assert.That(entity.Name, Is.EqualTo(dto.Name));
            Assert.That(entity.Minutes, Is.EqualTo(dto.Minutes));
        }

        [Test]
        public void FromEntity_ReturnsValidPlanDto()
        {
            var entity = new PlanDto()
            {
                Name = "Plan Test",
                Minutes = 10
            }.ToEntity();

            var dto = PlanDto.FromEntity(entity);

            Assert.IsNotNull(dto);
            Assert.That(dto, Is.TypeOf<PlanDto>());
            Assert.That(dto.Name, Is.EqualTo(entity.Name));
            Assert.That(dto.Minutes, Is.EqualTo(entity.Minutes));
            Assert.That(dto.Id, Is.EqualTo(entity.Id));
        }
    }
}