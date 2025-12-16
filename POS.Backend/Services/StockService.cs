using POS.Backend.Repositories;
using POS.Shared.DTOs.Stock;
using POS.Shared.Entities;
using POS.Shared.Enums;

namespace POS.Backend.Services
{
    public class StockService(IStockRepository stockRepository, IProductRepository productRepository) : IStockService
    {
        private readonly IStockRepository _stockRepository = stockRepository;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task AdjustStockAsync(CreateStockMovementDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId);
             if (product == null)
            {
                throw new Exception($"Product with ID {dto.ProductId} not found.");
            }

            if (dto.MovementType == MovementType.Entrada)
            {
                product.CurrentStock += dto.Quantity;
            }
            else if (dto.MovementType == MovementType.Salida)
            {
                 if (product.CurrentStock < dto.Quantity)
                {
                    throw new Exception($"Insufficient stock for product {product.Name}. Available: {product.CurrentStock}, Requested: {dto.Quantity}");
                }
                product.CurrentStock -= dto.Quantity;
            }
            // For now, if adjustment, we assume explicit +/- logic, but we stick to Entrada/Salida as primary actions. 
            // If specific "Adjust" (Set specific value) is needed, logic would differ. Assuming usage of Entrada/Salida for HU-006.

            await _productRepository.UpdateAsync(product);

            var movement = new StockMovement
            {
                ProductId = dto.ProductId,
                MovementDate = DateTime.Now,
                MovementType = dto.MovementType,
                Quantity = dto.Quantity,
                Notes = dto.Notes,
                UserId = dto.UserId
            };

            await _stockRepository.AddMovementAsync(movement);
        }
    }
}
