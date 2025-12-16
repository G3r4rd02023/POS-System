using POS.Backend.Repositories;
using POS.Shared.DTOs.Category;
using POS.Shared.Entities;

namespace POS.Backend.Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null!;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };
            var addedCategory = await _categoryRepository.CreateAsync(category);
            return new CategoryDto
            {
                Id = addedCategory.Id,
                Name = addedCategory.Name,
                Description = addedCategory.Description
            };
        }

        public async Task<CategoryDto> UpdateAsync(int id, EditCategoryDto request)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null!;

            category.Name = request.Name;
            category.Description = request.Description;

            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            return new CategoryDto
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                Description = updatedCategory.Description
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }
    }
}
