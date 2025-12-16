using Microsoft.AspNetCore.Mvc;
using POS.Backend.Services;
using POS.Shared.DTOs.User;

namespace POS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, IAuthService authService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IAuthService _authService = authService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto request)
        {
            var created = await _userService.CreateUserAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EditUserDto request)
        {
            if (request == null || id != request.Id) return BadRequest();



            var updated = await _userService.UpdateUserAsync(id, request);

            var result = new UserResponseDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Email = updated.Email,
                Username = updated.Username,
                Role = updated.Role,
                IsActive = updated.IsActive
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _userService.DeleteUserAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
