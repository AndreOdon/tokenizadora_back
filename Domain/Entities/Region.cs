using Domain.Dto.Result;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class Region : BaseEntity
    {
        [ExcludeFromCodeCoverage]
        public Region()
        { }

        public Region(RegionDto dto)
        {
            Name = dto.Name;
            Code = dto.Code;
        }

        public string Name { get; private set; }
        public string Code { get; private set; }

        [ExcludeFromCodeCoverage]
        public virtual List<RegionTax> IsOrigin { get; set; }
    }
}