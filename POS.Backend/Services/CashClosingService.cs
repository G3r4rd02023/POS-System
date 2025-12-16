using POS.Backend.Repositories;
using POS.Shared.DTOs.CashClosing;
using POS.Shared.Entities;

namespace POS.Backend.Services
{
    public class CashClosingService(ICashClosingRepository cashClosingRepository, ISaleRepository saleRepository) : ICashClosingService
    {
        private readonly ICashClosingRepository _cashClosingRepository = cashClosingRepository;
        private readonly ISaleRepository _saleRepository = saleRepository;

        public async Task<CashClosingDto> GetCurrentShiftInfoAsync()
        {
            var (startDate, sales) = await GetShiftDataAsync();

            decimal totalCash = sales.Sum(s => s.TotalAmount); // Assuming all sales are Cash for now as per simplified logic, or we'd check PaymentType if it existed.
            // Requirement says "Total vendido" (Total Sold).
            // DTO has TotalCash, TotalCard etc. 
            // Since Payment Method is not explicit in Sale entity yet (only TotalAmount), assuming all is Cash or Generic 'Total'.
            // For now mapping TotalAmount -> TotalCash.
            
            return new CashClosingDto
            {
                StartDate = startDate,
                EndDate = DateTime.Now,
                TotalCash = totalCash,
                InitialBalance = 0 // Needs lookup if we carried over. For now 0 or from last closing's final balance ??
                // Usually InitialBalance = Last Closing FinalBalance.
            };
        }

        public async Task<CashClosingDto> PerformCashClosingAsync(CreateCashClosingDto request)
        {
            var (startDate, sales) = await GetShiftDataAsync();
            var endDate = DateTime.Now;

            // Logic to determine Initial Balance (e.g. from previous closing)
            // For simplicity, 0 if no previous closing, or we could fetch it.
            var lastClosing = await _cashClosingRepository.GetLastClosingAsync();
            decimal initialBalance = lastClosing?.FinalBalance ?? 0; // Or FinalCash? adjusting 'InitialBalance' usually means the cash drawer start.
            // Let's assume InitialBalance is 0 for this iteration or carried over.
            
            decimal totalSales = sales.Sum(s => s.TotalAmount);
            
            // Calculate System Total
            decimal systemExpectedCash = initialBalance + totalSales; 

            // Calculate Difference
            decimal difference = request.FinalCash - systemExpectedCash;

            var cashClosing = new CashClosing
            {
                StartDate = startDate,
                EndDate = endDate,
                InitialBalance = initialBalance,
                TotalCash = totalSales, // Treating all sales as cash for MVP as per Entity structure (no PaymentMethod breakdown yet)
                TotalCard = 0,
                TotalTransfer = 0,
                TotalAdjustments = difference,
                FinalBalance = systemExpectedCash, // The system calculated balance? Or the actual?
                // Usually FinalBalance = System Expected. FinalCash = Physical Count.
                FinalCash = request.FinalCash,
                UserId = request.UserId,
                Notes = request.Notes ?? string.Empty
            };

            await _cashClosingRepository.CreateAsync(cashClosing);

            return MapToDto(cashClosing);
        }

        private async Task<(DateTime StartDate, IEnumerable<Sale> Sales)> GetShiftDataAsync()
        {
            var lastClosing = await _cashClosingRepository.GetLastClosingAsync();
            var startDate = lastClosing?.EndDate ?? DateTime.Today; // If no closing, from today start? Or from beginning of time? 'Today' is safer fallback.
            
            if (lastClosing == null)
            {
                // If never closed, maybe all sales? or just today.
                // Let's assume 'Today' to avoid processing ancient history if DB is old.
                startDate = new DateTime(1900, 1, 1); // Safe date for SQL Server datetime
            }

            var endDate = DateTime.Now;
            var sales = await _saleRepository.GetSalesByDateRangeAsync(startDate, endDate);
            return (startDate, sales);
        }

        private static CashClosingDto MapToDto(CashClosing entity)
        {
            return new CashClosingDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                InitialBalance = entity.InitialBalance,
                TotalCash = entity.TotalCash,
                TotalCard = entity.TotalCard,
                TotalTransfer = entity.TotalTransfer,
                TotalAdjustments = entity.TotalAdjustments,
                FinalBalance = entity.FinalBalance,
                FinalCash = entity.FinalCash,
                Notes = entity.Notes,
                UserName = entity.User?.Name ?? string.Empty
            };
        }
    }
}
