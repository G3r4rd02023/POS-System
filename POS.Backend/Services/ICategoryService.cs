using POS.Shared.DTOs.Category;

namespace POS.Backend.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto request);
        Task<CategoryDto> UpdateAsync(int id, EditCategoryDto request);
        Task<bool> DeleteAsync(int id);
    }
}
