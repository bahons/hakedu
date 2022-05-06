using auth.Attributes;
using auth.DbModels;
using auth.Models;
using auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateRequest model)
        {
            var result = await _userService.Register(model);
            return Ok(result);
        }



        [Authorize]
        [HttpGet("User")]
        public async Task<IActionResult> GetUser(HttpContext httpContext)
        {
            var user = await _userService.(httpContext.Items);
            return Ok(user);
        }
    }
}
