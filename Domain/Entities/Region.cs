namespace Domain.Entities
{
    public class Region : BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public virtual List<RegionTax> IsOrigin { get; set; }
    }
}