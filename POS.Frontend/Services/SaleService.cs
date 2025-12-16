using POS.Shared.DTOs.Sale;
using System.Text.Json;

namespace POS.Frontend.Services
{
    public class SaleService(IRequestService requestService) : ISaleService
    {
        private readonly IRequestService _requestService = requestService;
        private const string BaseUrl = "api/sales";
        private static JsonSerializerOptions JsonDefaultOptions => new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<SaleDto> CreateSaleAsync(CreateSaleDto request)
        {
             var response = await _requestService.PostAsync(BaseUrl, request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SaleDto>(content, JsonDefaultOptions)!;
            }
            return null!;
        }

        public async Task<IEnumerable<SaleDto>> GetReportAsync(SaleFilterDto filter)
        {
            var response = await _requestService.PostAsync($"{BaseUrl}/report", filter);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<SaleDto>>(content, JsonDefaultOptions)!;
            }
            return new List<SaleDto>();
        }
    }
}
