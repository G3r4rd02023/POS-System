using POS.Shared.Enums;

namespace POS.Shared.DTOs.User
{
    public class EditUserDto
    {
        public int Id { get; set; }

        public Role Role { get; set; }

        public bool IsActive { get; set; }
    }
}
