using POS.Shared.DTOs.Stock;

namespace POS.Frontend.Services
{
    public interface IStockService
    {
        Task AdjustStockAsync(CreateStockMovementDto request);
    }
}
