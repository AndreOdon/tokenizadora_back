using Domain.Dto.Result;
using Domain.Interfaces.Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<PlanDto>>> GetAll()
        {
            var plans = await _planService.GetAllPlans();
            return Ok(plans);
        }
    }
}