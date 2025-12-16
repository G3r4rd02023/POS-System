using POS.Backend.Repositories;
using POS.Shared.DTOs.Sale;
using POS.Shared.Entities;

namespace POS.Backend.Services
{
    public class SaleService(ISaleRepository saleRepository, IProductRepository productRepository) : ISaleService
    {
        private readonly ISaleRepository _saleRepository = saleRepository;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<SaleDto> CreateSaleAsync(CreateSaleDto request)
        {
            var sale = new Sale
            {
                UserId = request.UserId,
                SaleDate = DateTime.Now,
                SaleDetails = new List<SaleDetail>(),
                PaymentMethod = request.PaymentMethod // Set PaymentMethod
            };

            decimal totalAmount = 0;

            foreach (var detailDto in request.SaleDetails)
            {
                var product = await _productRepository.GetByIdAsync(detailDto.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {detailDto.ProductId} not found.");
                }

                if (product.CurrentStock < detailDto.Quantity)
                {
                    throw new Exception($"Insufficient stock for product {product.Name}. Available: {product.CurrentStock}, Requested: {detailDto.Quantity}");
                }

                // Update Stock
                product.CurrentStock -= detailDto.Quantity;
                await _productRepository.UpdateAsync(product);

                var saleDetail = new SaleDetail
                {
                    ProductId = detailDto.ProductId,
                    Quantity = detailDto.Quantity,
                    UnitPrice = product.Price, 
                    TotalPrice = product.Price * detailDto.Quantity
                };
                
                totalAmount += saleDetail.TotalPrice;
                sale.SaleDetails.Add(saleDetail);
            }

            sale.TotalAmount = totalAmount;

            await _saleRepository.CreateAsync(sale);

            return MapToDto(sale);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesByFilterAsync(SaleFilterDto filter)
        {
            var sales = await _saleRepository.GetSalesByFilterAsync(filter);
            return sales.Select(MapToDto);
        }

        private static SaleDto MapToDto(Sale sale) => new SaleDto
        {
                Id = sale.Id,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                UserName = sale.User?.Name,
                SaleDetails = sale.SaleDetails.Select(d => new SaleDetailDto
                {
                    ProductId = d.ProductId,
                    ProductName = d.Product?.Name ?? string.Empty, 
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    TotalPrice = d.TotalPrice
                }).ToList()
        };
    }
}
