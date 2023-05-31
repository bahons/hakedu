using auth.Attributes;
using auth.DbModels;
using auth.Middleware;
using auth.Models;
using auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private readonly UserData _userData;

        public AuthController(IUserService userService, UserData userData)
        {
            _userService = userService;
            _userData = userData;
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
        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> GetUser()
        {
            return new JsonResult(_userData.Id);
        }
    }
}
