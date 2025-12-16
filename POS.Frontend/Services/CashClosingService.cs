using POS.Shared.DTOs.CashClosing;
using System.Text.Json;

namespace POS.Frontend.Services
{
    public class CashClosingService(IRequestService requestService) : ICashClosingService
    {
        private readonly IRequestService _requestService = requestService;
        private const string BaseUrl = "api/cashclosing";
        private static JsonSerializerOptions JsonDefaultOptions => new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<CashClosingDto?> GetPreviewAsync()
        {
             return await _requestService.GetAsync<CashClosingDto>($"{BaseUrl}/preview");
        }

        public async Task<CashClosingDto?> PerformClosingAsync(CreateCashClosingDto request)
        {
            var response = await _requestService.PostAsync(BaseUrl, request);
             if (response.IsSuccessStatusCode)
             {
                 var content = await response.Content.ReadAsStringAsync();
                 return JsonSerializer.Deserialize<CashClosingDto>(content, JsonDefaultOptions);
             }
             return null;
        }
    }
}
