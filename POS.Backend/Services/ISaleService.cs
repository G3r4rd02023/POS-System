using POS.Shared.DTOs.Sale;
using POS.Shared.Entities; // Needed for return type

namespace POS.Backend.Services
{
    public interface ISaleService
    {
        Task<SaleDto> CreateSaleAsync(CreateSaleDto request);
        Task<IEnumerable<SaleDto>> GetSalesByFilterAsync(SaleFilterDto filter);
    }
}
