using POS.Shared.DTOs.Sale;

namespace POS.Frontend.Services
{
    public interface ISaleService
    {
         Task<SaleDto> CreateSaleAsync(CreateSaleDto request);
         Task<IEnumerable<SaleDto>> GetReportAsync(SaleFilterDto filter);
    }
}
