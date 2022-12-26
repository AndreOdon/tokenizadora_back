namespace Domain.Dto.Input
{
    public class CalcInputDto
    {
        public int PlanId { get; set; }
        public int OriginId { get; set; }
        public int DestinyId { get; set; }
        public int Minutes { get; set; }
    }
}