using Domain.Entities;

namespace Domain.Dto.Result
{
    public class RegionTaxDto
    {
        public int OriginRegionId { get; set; }
        public int DestinyRegionId { get; set; }
        public decimal Tax { get; set; }

        public RegionTax ToEntity()
        {
            return new RegionTax(this);
        }
    }
}