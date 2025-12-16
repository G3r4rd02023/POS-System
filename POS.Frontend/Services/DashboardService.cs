using POS.Shared.DTOs.Dashboard;
using System.Text.Json;

namespace POS.Frontend.Services
{
    public class DashboardService(IRequestService requestService) : IDashboardService
    {
        private readonly IRequestService _requestService = requestService;
        private const string BaseUrl = "api/dashboard";
        private static JsonSerializerOptions JsonDefaultOptions => new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<DashboardDataDto?> GetDataAsync()
        {
            var response = await _requestService.GetAsync<DashboardDataDto>(BaseUrl); // Assuming generic GetAsync returns T or use object/httpresponse variant logic
            // Note: RequestService GetAsync signature varied in prev steps. 
            // My recent fix was: Task<T> GetAsync<T>(string url); 
            // So:
            return response;
        }
    }
}
