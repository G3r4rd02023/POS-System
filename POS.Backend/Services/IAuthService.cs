using Microsoft.AspNetCore.Identity.Data;
using POS.Shared.DTOs.Auth;
using POS.Shared.DTOs.User;

namespace POS.Backend.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(AuthRequestDto request);

        Task<UserResponseDto> Register(CreateUserDto request);
    }
}
