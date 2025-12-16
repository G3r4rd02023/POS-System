using POS.Shared.DTOs.Dashboard;

namespace POS.Frontend.Services
{
    public interface IDashboardService
    {
        Task<DashboardDataDto?> GetDataAsync();
    }
}
