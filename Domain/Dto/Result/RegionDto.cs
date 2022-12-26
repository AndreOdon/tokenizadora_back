using Domain.Entities;

namespace Domain.Dto.Result
{
    public class RegionDto
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Region ToEntity()
        {
            return new Region(this);
        }
    }
}