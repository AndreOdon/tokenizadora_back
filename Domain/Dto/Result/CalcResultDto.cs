namespace Domain.Dto.Result
{
    public class CalcResultDto
    {
        public decimal? WithoutPlanValue { get; set; }
        public decimal? WithPlanValue { get; set; }
        public string Error { get; set; }
    }
}