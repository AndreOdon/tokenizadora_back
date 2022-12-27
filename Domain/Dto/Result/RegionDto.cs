using Domain.Entities;

namespace Domain.Dto.Result
{
    public class RegionDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public Region ToEntity()
        {
            return new Region(this);
        }

        public static RegionDto FromEntity(Region entity)
        {
            return new RegionDto()
            {
                Name = entity.Name,
                Code = entity.Code,
                Id = entity.Id
            };
        }
    }
}