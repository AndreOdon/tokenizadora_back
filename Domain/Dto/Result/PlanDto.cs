using Domain.Entities;

namespace Domain.Dto.Result
{
    public class PlanDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }

        public static PlanDto FromEntity(Plan entity)
        {
            return new PlanDto()
            {
                Name = entity.Name,
                Minutes = entity.Minutes,
                Id = entity.Id
            };
        }

        public Plan ToEntity()
        {
            return new Plan(this);
        }
    }
}