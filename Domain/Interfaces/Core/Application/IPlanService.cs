using Domain.Dto.Input;
using Domain.Dto.Result;

namespace Domain.Interfaces.Core.Application
{
    public interface IPlanService
    {
        Task<CalcResultDto> Calc(CalcInputDto calcInputDto);

        Task<List<PlanDto>> GetAllPlans();
    }
}