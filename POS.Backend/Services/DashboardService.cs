using Microsoft.EntityFrameworkCore;
using POS.Backend.Data;
using POS.Shared.DTOs.Dashboard;
using POS.Shared.DTOs.Product;
using POS.Shared.DTOs.Category; // Needed if mapping involves it or plain object

namespace POS.Backend.Services
{
    public class DashboardService(AppDbContext context) : IDashboardService
    {
        private readonly AppDbContext _context = context;

        public async Task<DashboardDataDto> GetDataAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            // 1. Sales metrics for Today
            var salesToday = await _context.Sales
                .Where(s => s.SaleDate >= today && s.SaleDate < tomorrow)
                .Select(s => new { s.SaleDate, s.TotalAmount })
                .ToListAsync();

            decimal totalSales = salesToday.Sum(s => s.TotalAmount);
            int transactions = salesToday.Count;
            decimal avgTicket = transactions > 0 ? totalSales / transactions : 0;

            // 2. Sales by Hour (Chart)
            // Group in memory for simplicity or EF 
            var salesByHour = salesToday
                .GroupBy(s => s.SaleDate.Hour)
                .Select(g => new HourlySaleDto
                {
                    Hour = FormatHour(g.Key),
                    TotalAmount = g.Sum(s => s.TotalAmount)
                })
                .OrderBy(h => ParseHour(h.Hour)) // Helper to keep order 
                .ToList();

            // Fill missing hours? For now, just present ones.
            
            // 3. Low Stock 
            var lowStockProducts = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CurrentStock <= p.StockMinimum)
                .Take(5)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CurrentStock = p.CurrentStock,
                    StockMinimum = p.StockMinimum, // Add this to ProductDto if not exists or reuse existing
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    CategoryName = p.Category != null ? p.Category.Name : string.Empty
                })
                .ToListAsync();

            int lowStockCount = await _context.Products.CountAsync(p => p.CurrentStock <= p.StockMinimum);

            // 4. Top Selling (Simplified: by TotalQuantity in SaleDetails)
            // Note: This matches "Current Trends" or "Most Sold"
            var topSelling = await _context.SaleDetails
                .GroupBy(sd => sd.ProductId)
                .Select(g => new 
                { 
                    ProductId = g.Key, 
                    TotalSold = g.Sum(sd => sd.Quantity) 
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .Join(_context.Products.Include(p => p.Category), 
                      sd => sd.ProductId, 
                      p => p.Id, 
                      (sd, p) => new ProductDto 
                      { 
                          Id = p.Id, 
                          Name = p.Name, 
                          Price = p.Price, 
                          CurrentStock = p.CurrentStock,
                          ImageUrl = p.ImageUrl,
                          CategoryName = p.Category != null ? p.Category.Name : string.Empty
                          // Could add "SoldQuantity" to DTO if needed specific for dashboard
                      })
                .ToListAsync();

            return new DashboardDataDto
            {
                TotalSalesToday = totalSales,
                TransactionsToday = transactions,
                AverageTicketToday = avgTicket,
                LowStockCount = lowStockCount,
                SalesByHour = salesByHour,
                LowStockProducts = lowStockProducts,
                TopSellingProducts = topSelling
            };
        }

        private static string FormatHour(int hour)
        {
            if (hour == 0) return "12am";
            if (hour == 12) return "12pm";
            if (hour < 12) return $"{hour}am";
            return $"{hour - 12}pm";
        }

        private static int ParseHour(string hourStr)
        {
            // Simple helper for sorting if needed, logic depends on string format
            return 0; // Simplified for now
        }
    }
}
