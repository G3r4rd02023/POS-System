using POS.Shared.DTOs.Stock;

namespace POS.Backend.Services
{
    public interface IStockService
    {
        Task AdjustStockAsync(CreateStockMovementDto dto);
    }
}
