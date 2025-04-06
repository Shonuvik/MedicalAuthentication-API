using MedicalAuthenticationAPI.Controllers.V1;
using MedicalAuthenticationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAuthenticationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(UserCreateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(UserCreateDto userCreate)
        {
            try
            {
                var createdUser = await _userService.CreateAsync(userCreate);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserDto user)
        {
            try
            {
                return Ok(await _userService.LoginAsync(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

