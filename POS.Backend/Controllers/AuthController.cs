using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using POS.Backend.Services;
using POS.Shared.DTOs.Auth;
using POS.Shared.DTOs.User;

namespace POS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto
            
            request)
        {
            if (request == null)
            {
                return BadRequest("Invalid login request");
            }
            var response = await _authService.Login(request);
            if (response.IsAuthenticated)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized(response.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid registration request");
            }
            var response = await _authService.Register(request);
            if (response != null)
            {
                return CreatedAtAction(nameof(Register), new { id = response.Id }, response);
            }
            else
            {
                return BadRequest("Registration failed");
            }
        }
    }
}
