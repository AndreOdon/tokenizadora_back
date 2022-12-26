using Domain.Dto.Result;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class Plan : BaseEntity
    {
        [ExcludeFromCodeCoverage]
        public Plan()
        { }

        public Plan(PlanDto planDto)
        {
            Name = planDto.Name;
            Minutes = planDto.Minutes;
        }

        public string Name { get; private set; }
        public int Minutes { get; private set; }
    }
}