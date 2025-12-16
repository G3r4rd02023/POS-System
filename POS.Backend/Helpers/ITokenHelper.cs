using POS.Shared.Entities;

namespace POS.Backend.Helpers
{
    public interface ITokenHelper
    {
        public string GenerateToken(User user);
    }
}
