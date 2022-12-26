using Domain.Dto.Result;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class RegionTax : BaseEntity
    {
        [ExcludeFromCodeCoverage]
        public RegionTax()
        { }

        public RegionTax(RegionTaxDto regionTaxDto)
        {
            OriginRegionId = regionTaxDto.OriginRegionId;
            DestinyRegionId = regionTaxDto.DestinyRegionId;
            Tax = regionTaxDto.Tax;
        }

        public int OriginRegionId { get; private set; }
        public int DestinyRegionId { get; private set; }
        public decimal Tax { get; private set; }

        [ExcludeFromCodeCoverage]
        public virtual Region Origin { get; set; }
        [ExcludeFromCodeCoverage]
        public virtual Region Destiny { get; set; }
    }
}