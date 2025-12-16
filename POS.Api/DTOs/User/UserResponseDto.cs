using POS.Shared.Enums;

namespace POS.Shared.DTOs.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public Role Role { get; set; }

        public bool IsActive { get; set; }
    }
}
