using POS.Shared.Entities;

namespace POS.Backend.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(int id);

        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> CreateAsync(Category category);

        Task<Category> UpdateAsync(Category category);

        Task<bool> DeleteAsync(int id);

        Task<bool> SaveChangesAsync();
    }
}
