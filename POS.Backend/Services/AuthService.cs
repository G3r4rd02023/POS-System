using Microsoft.AspNetCore.Identity.Data;
using POS.Backend.Helpers;
using POS.Backend.Repositories;
using POS.Shared.DTOs.Auth;
using POS.Shared.DTOs.User;
using POS.Shared.Entities;

namespace POS.Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;

        public AuthService(IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<AuthResponseDto> Login(AuthRequestDto request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Username);
            var isAuthenticated = user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (isAuthenticated && user!.IsActive)
            {
                var token = _tokenHelper.GenerateToken(user!);
                return new AuthResponseDto { IsAuthenticated = true, Token = token, Message = "Inicio de sesion exitoso" };
            }
            else
            {
                return new AuthResponseDto { IsAuthenticated = false, Token = string.Empty, Message = "Credenciales incorrectas" };
            }
        }

        public async Task<UserResponseDto> Register(CreateUserDto request)
        {
            var user = new User
            {
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                IsActive = request.IsActive
                
            };

            var createdUser = await _userRepository.CreateAsync(user);

            var response = new UserResponseDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email,
                Role = createdUser.Role,
                IsActive = createdUser.IsActive,
                Username = createdUser.Username
            };

            return response;
        }
    }
}
