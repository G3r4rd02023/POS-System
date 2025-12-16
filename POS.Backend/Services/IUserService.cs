using POS.Shared.DTOs.User;

namespace POS.Backend.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(CreateUserDto request);
        Task<UserResponseDto> GetByIdAsync(int id);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> UpdateUserAsync(int id, EditUserDto request);
    }
}