using POS.Shared.DTOs.Stock;
using System.Text.Json;

namespace POS.Frontend.Services
{
    public class StockService(IRequestService requestService) : IStockService
    {
        private readonly IRequestService _requestService = requestService;
        private const string BaseUrl = "api/stock/adjust";

        public async Task AdjustStockAsync(CreateStockMovementDto request)
        {
            var response = await _requestService.PostAsync(BaseUrl, request);
            if (!response.IsSuccessStatusCode)
            {
               throw new Exception("Failed to adjust stock");
            }
        }
    }
}
