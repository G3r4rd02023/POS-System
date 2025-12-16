using POS.Frontend.Helpers;
using POS.Shared.DTOs.Category;
using System.Text.Json;

namespace POS.Frontend.Services
{
    public class CategoryService(IRequestService requestService) : ICategoryService
    {
        private readonly IRequestService _requestService = requestService;
        private const string BaseUrl = "api/categories";
        private static JsonSerializerOptions JsonDefaultOptions => new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return await _requestService.GetAsync<IEnumerable<CategoryDto>>(BaseUrl);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            return await _requestService.GetByIdAsync<CategoryDto>(BaseUrl, id);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto request)
        {
            var response = await _requestService.PostAsync(BaseUrl, request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CategoryDto>(content, JsonDefaultOptions)!;
            }
            return null!;
        }

        public async Task<CategoryDto> UpdateAsync(int id, EditCategoryDto request)
        {
            var response = await _requestService.PutAsync($"{BaseUrl}/{id}", request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CategoryDto>(content, JsonDefaultOptions)!;
            }
            return null!;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _requestService.DeleteAsync($"{BaseUrl}/{id}");
            // DeleteAsync returns object, but we just need to know if it succeeded.
             // Usually RequestService throws if not success, or we should check for success.
             // Looking at RequestService implementation, it calls EnsureSuccessStatusCode.
            return true;
        }
    }
}
