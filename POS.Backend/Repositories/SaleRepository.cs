using POS.Backend.Data;
using POS.Shared.DTOs.Sale;
using POS.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace POS.Backend.Repositories
{
    public class SaleRepository(AppDbContext context) : ISaleRepository
    {
        private readonly AppDbContext _context = context;

        public async Task CreateAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByFilterAsync(SaleFilterDto filter)
        {
            var query = _context.Sales
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.Product) // Include product to filter by Category
                .Include(s => s.User)
                .AsQueryable();

            if (filter.StartDate.HasValue)
                query = query.Where(s => s.SaleDate >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(s => s.SaleDate <= filter.EndDate.Value);

            if (filter.UserId.HasValue)
                query = query.Where(s => s.UserId == filter.UserId.Value);

            if (filter.PaymentMethod.HasValue)
                query = query.Where(s => s.PaymentMethod == filter.PaymentMethod.Value);

            if (filter.CategoryId.HasValue)
            {
                // Filter sales where AT LEAST one detail belongs to the category
                query = query.Where(s => s.SaleDetails.Any(sd => sd.Product.CategoryId == filter.CategoryId.Value));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Sales
                .Include(s => s.SaleDetails) // Include details if needed for advanced reporting, though TotalAmount is on Sale
                .Include(s => s.User)
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .ToListAsync();
        }
    }
}
