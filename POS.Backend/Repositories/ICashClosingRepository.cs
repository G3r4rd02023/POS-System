using POS.Shared.Entities;

namespace POS.Backend.Repositories
{
    public interface ICashClosingRepository
    {
        Task<CashClosing?> GetLastClosingAsync();
        Task CreateAsync(CashClosing closing);
    }
}
