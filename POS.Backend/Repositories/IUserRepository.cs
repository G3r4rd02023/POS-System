using POS.Shared.Entities;

namespace POS.Backend.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);

        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserByUsernameAsync(string username);
        
        Task<bool> ExistsByEmailAsync(string email);

        Task<bool> ExistsByUsernameAsync(string username);

        Task<bool> DeleteAsync(int id);

        Task<bool> SaveChangesAsync();
    }
}
