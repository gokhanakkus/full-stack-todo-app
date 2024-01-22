using Microsoft.AspNetCore.Mvc;
using Todo.Services.Abstract;
using Todo.Services.Models.Auth;
using Todo.Web.Api.Models;

namespace Todo.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }

        public IAuthService AuthService { get; }

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResult = await AuthService.LoginAsync(loginModel.Username, loginModel.Password, cancellationToken);
            if (!serviceResult.IsSuccess)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.Message);
            }

            return Ok(serviceResult.Data);
        }
    }
}
