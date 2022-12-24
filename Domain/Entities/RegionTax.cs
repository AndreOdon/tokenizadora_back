namespace Domain.Entities
{
    public class RegionTax : BaseEntity
    {
        public int OriginRegionId { get; private set; }
        public int DestinyRegionId { get; private set; }
        public double Tax { get; private set; }

        public virtual Region Origin { get; set; }
        public virtual Region Destiny { get; set; }
    }
}