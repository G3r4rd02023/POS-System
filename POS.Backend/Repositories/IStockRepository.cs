using POS.Shared.Entities;

namespace POS.Backend.Repositories
{
    public interface IStockRepository
    {
        Task AddMovementAsync(StockMovement movement);
    }
}
