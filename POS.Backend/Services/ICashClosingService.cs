using POS.Shared.DTOs.CashClosing;

namespace POS.Backend.Services
{
    public interface ICashClosingService
    {
        Task<CashClosingDto> GetCurrentShiftInfoAsync();
        Task<CashClosingDto> PerformCashClosingAsync(CreateCashClosingDto request);
    }
}
