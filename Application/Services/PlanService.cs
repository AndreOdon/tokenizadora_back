using Domain.Dto.Input;
using Domain.Dto.Result;
using Domain.Interfaces.Adapters.Infra.DataBase;
using Domain.Interfaces.Core.Application;

namespace Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IRegionTaxRepository _regionTaxRepository;

        public PlanService(IPlanRepository planRepository,
            IRegionTaxRepository regionTaxRepository)
        {
            _planRepository = planRepository;
            _regionTaxRepository = regionTaxRepository;
        }

        public async Task<CalcResultDto> Calc(CalcInputDto calcInputDto)
        {
            CalcResultDto result = new CalcResultDto();

            var planEntity = await _planRepository.GetById(calcInputDto.PlanId);

            if (planEntity is null)
            {
                result.Error = "Plano não encontrado";
                return result;
            }

            var taxEntity = await _regionTaxRepository.GetByOriginDestiny(calcInputDto.OriginId, calcInputDto.DestinyId);
            if (taxEntity is null)
            {
                result.Error = "Taxa para origem e destino não encontrada";
                return result;
            }

            result.WithoutPlanValue = taxEntity.Tax * calcInputDto.Minutes;
            result.WithPlanValue = CalcValueWithPlan(planEntity.Minutes, calcInputDto.Minutes, taxEntity.Tax);

            return result;
        }

        private decimal CalcValueWithPlan(int planMinutes, int minutesUsed, decimal tax)
        {
            if (minutesUsed <= planMinutes)
            {
                return 0;
            }

            return decimal.Round((minutesUsed - planMinutes) * (tax + (tax * 0.1M)), 2);
        }
    }
}