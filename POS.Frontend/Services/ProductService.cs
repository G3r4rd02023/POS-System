using POS.Frontend.Helpers;
using POS.Shared.DTOs.Product;
using System.Text.Json;

namespace POS.Frontend.Services
{
    public class ProductService(IRequestService requestService) : IProductService
    {
        private readonly IRequestService _requestService = requestService;
        private const string BaseUrl = "api/products";
        private static JsonSerializerOptions JsonDefaultOptions => new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _requestService.GetAsync<IEnumerable<ProductDto>>(BaseUrl);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return await _requestService.GetByIdAsync<ProductDto>(BaseUrl, id);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto request)
        {
            var response = await _requestService.PostAsync(BaseUrl, request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ProductDto>(content, JsonDefaultOptions)!;
            }
            return null!;
        }

        public async Task<ProductDto> UpdateAsync(int id, EditProductDto request)
        {
            var response = await _requestService.PutAsync($"{BaseUrl}/{id}", request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ProductDto>(content, JsonDefaultOptions)!;
            }
            return null!;
        }

        public async Task<bool> DeleteAsync(int id)
        {
             // DeleteAsync returns object. If successful no specific return type needed here.
            await _requestService.DeleteAsync($"{BaseUrl}/{id}");
            return true;
        }
    }
}
