namespace Domain.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; private set; }
        public int Minutes { get; private set; }
    }
}