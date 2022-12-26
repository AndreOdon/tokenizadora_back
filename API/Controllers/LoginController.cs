using Domain.Dto.Input;
using Domain.Dto.Result;
using Domain.Interfaces.Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(UserLoginInputDto inputDto)
        {
            var result = await _userService.Login(inputDto);

            if (result is null)
                return BadRequest("Login e/ou senha incorretos");

            return Ok(result);
        }
    }
}