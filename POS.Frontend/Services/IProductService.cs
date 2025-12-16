using POS.Shared.DTOs.Product;

namespace POS.Frontend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductDto request);
        Task<ProductDto> UpdateAsync(int id, EditProductDto request);
        Task<bool> DeleteAsync(int id);
    }
}
