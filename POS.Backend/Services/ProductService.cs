using POS.Backend.Helpers;
using POS.Backend.Repositories;
using POS.Shared.DTOs.Product;
using POS.Shared.Entities;

namespace POS.Backend.Services
{
    public class ProductService(IProductRepository productRepository, IPhotoHelper photoHelper, ICategoryRepository categoryRepository) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IPhotoHelper _photoHelper = photoHelper;

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CurrentStock = p.CurrentStock,
                StockMinimum = p.StockMinimum,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty,
                IsActive = p.IsActive,
                ImageUrl = p.ImageUrl
            });
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null!;

            return new ProductDto
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CurrentStock = product.CurrentStock,
                StockMinimum = product.StockMinimum,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty,
                IsActive = product.IsActive,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto request)
        {
            // Verify category exists
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            var product = new Product
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CurrentStock = request.CurrentStock,
                StockMinimum = request.StockMinimum,
                CategoryId = request.CategoryId,
                IsActive = true
            };

            if (!string.IsNullOrEmpty(request.Image))
            {
                product.ImageUrl = await _photoHelper.UploadImage(request.Image);
            }

            var createdProduct = await _productRepository.CreateAsync(product);
            // Re-fetch to get Category info or just construct DTO
             createdProduct.Category = category;

            return new ProductDto
            {
                Id = createdProduct.Id,
                Code = createdProduct.Code,
                Name = createdProduct.Name,
                Description = createdProduct.Description,
                Price = createdProduct.Price,
                CurrentStock = createdProduct.CurrentStock,
                StockMinimum = createdProduct.StockMinimum,
                CategoryId = createdProduct.CategoryId,
                CategoryName = createdProduct.Category.Name,
                IsActive = createdProduct.IsActive,
                ImageUrl = createdProduct.ImageUrl
            };
        }

        public async Task<ProductDto> UpdateAsync(int id, EditProductDto request)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null!;

             var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            product.Code = request.Code;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.CurrentStock = request.CurrentStock;
            product.StockMinimum = request.StockMinimum;
            product.CategoryId = request.CategoryId;
            product.IsActive = request.IsActive;

            if (!string.IsNullOrEmpty(request.Image))
            {
                 // Ideally delete old image first if exists, but for now just overwrite URL
                product.ImageUrl = await _photoHelper.UploadImage(request.Image);
            }

            var updatedProduct = await _productRepository.UpdateAsync(product);
             updatedProduct.Category = category;

            return new ProductDto
            {
                 Id = updatedProduct.Id,
                Code = updatedProduct.Code,
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                CurrentStock = updatedProduct.CurrentStock,
                StockMinimum = updatedProduct.StockMinimum,
                CategoryId = updatedProduct.CategoryId,
                CategoryName = updatedProduct.Category.Name,
                IsActive = updatedProduct.IsActive,
                ImageUrl = updatedProduct.ImageUrl
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
             // Optional: Delete image from Cloudinary
            return await _productRepository.DeleteAsync(id);
        }
    }
}
