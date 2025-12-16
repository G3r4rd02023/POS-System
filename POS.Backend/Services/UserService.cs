using POS.Backend.Repositories;
using POS.Shared.DTOs.User;
using POS.Shared.Entities;

namespace POS.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
                IsActive = user.IsActive
            });
        }

        public async Task<UserResponseDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }
        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                IsActive = request.IsActive
            };
            var userCreated = await _userRepository.CreateAsync(user);
            return new UserResponseDto
            {
                Id = userCreated.Id,
                Name = userCreated.Name,
                Email = userCreated.Email,
                Username = userCreated.Username,
                Role = userCreated.Role,
                IsActive = userCreated.IsActive
            };
        }

        public async Task<UserResponseDto> UpdateUserAsync(int id, EditUserDto request)
        {
            var user = await _userRepository.GetByIdAsync(id);

            user.IsActive = request.IsActive;
            user.Role = request.Role;
            var updatedUser = await _userRepository.UpdateAsync(user);
            return new UserResponseDto
            {
                Id = updatedUser.Id,
                Name = updatedUser.Name,
                Email = updatedUser.Email,
                Username = updatedUser.Username,
                Role = updatedUser.Role,
                IsActive = updatedUser.IsActive
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
