using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Backend.Services;
using POS.Shared.DTOs.Stock;

namespace POS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController(IStockService stockService) : ControllerBase
    {
        private readonly IStockService _stockService = stockService;

        [HttpPost("adjust")]
        public async Task<IActionResult> AdjustStock(CreateStockMovementDto request)
        {
            try
            {
                await _stockService.AdjustStockAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
