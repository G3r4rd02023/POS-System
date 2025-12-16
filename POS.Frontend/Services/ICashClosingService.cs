using POS.Shared.DTOs.CashClosing;

namespace POS.Frontend.Services
{
    public interface ICashClosingService
    {
        Task<CashClosingDto?> GetPreviewAsync();
        Task<CashClosingDto?> PerformClosingAsync(CreateCashClosingDto request);
    }
}
