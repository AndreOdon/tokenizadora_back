using Domain.Dto.Input;
using Domain.Dto.Result;
using Domain.Interfaces.Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorController : ControllerBase
    {
        private readonly IPlanService _planService;

        public SimulatorController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpPost("calc")]
        public async Task<ActionResult<CalcResultDto>> CalculateSimulation(CalcInputDto calcInputDto)
        {
            var result = await _planService.Calc(calcInputDto);

            if (result.Error is not null)
                return BadRequest(result.Error);

            return Ok(result);
        }
    }
}