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
                Name = "Region Test",
                Minutes = 10
            };

            var entity = dto.ToEntity();

            Assert.IsNotNull(entity);
            Assert.That(entity, Is.TypeOf<Plan>());
            Assert.That(entity.Name, Is.EqualTo(dto.Name));
            Assert.That(entity.Minutes, Is.EqualTo(dto.Minutes));
        }
    }
}