using POS.Backend.Data;
using POS.Shared.Entities;

namespace POS.Backend.Repositories
{
    public class StockRepository(AppDbContext context) : IStockRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddMovementAsync(StockMovement movement)
        {
            _context.StockMovements.Add(movement);
            await _context.SaveChangesAsync();
        }
    }
}
