using Domain.Dto.Result;
using Domain.Interfaces.Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<RegionDto>>> GetAll()
        {
            var regions = await _regionService.GetAllRegions();
            return Ok(regions);
        }
    }
}