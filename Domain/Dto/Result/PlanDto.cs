using Domain.Entities;

namespace Domain.Dto.Result
{
    public class PlanDto
    {
        public string Name { get; set; }
        public int Minutes { get; set; }

        public Plan ToEntity()
        {
            return new Plan(this);
        }
    }
}