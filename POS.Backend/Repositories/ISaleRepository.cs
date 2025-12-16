using POS.Shared.DTOs.Sale;
using POS.Shared.Entities;

namespace POS.Backend.Repositories
{
    public interface ISaleRepository
    {
        Task CreateAsync(Sale sale);
        Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Sale>> GetSalesByFilterAsync(SaleFilterDto filter);
    }
}
