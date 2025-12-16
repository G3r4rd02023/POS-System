using POS.Shared.DTOs.Dashboard;

namespace POS.Backend.Services
{
    public interface IDashboardService
    {
        Task<DashboardDataDto> GetDataAsync();
    }
}
