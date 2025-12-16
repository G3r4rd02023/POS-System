using POS.Shared.DTOs.Product;

namespace POS.Shared.DTOs.Dashboard
{
    public class DashboardDataDto
    {
        public decimal TotalSalesToday { get; set; }
        public int TransactionsToday { get; set; }
        public decimal AverageTicketToday { get; set; }
        public int LowStockCount { get; set; }
        public List<HourlySaleDto> SalesByHour { get; set; } = new();
        public List<ProductDto> LowStockProducts { get; set; } = new();
        public List<ProductDto> TopSellingProducts { get; set; } = new();
    }

    public class HourlySaleDto
    {
        public string Hour { get; set; } = string.Empty; // e.g., "10am", "2pm"
        public decimal TotalAmount { get; set; }
    }
}
