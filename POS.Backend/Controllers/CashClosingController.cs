using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Backend.Services;
using POS.Shared.DTOs.CashClosing;

namespace POS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CashClosingController(ICashClosingService cashClosingService) : ControllerBase
    {
        private readonly ICashClosingService _cashClosingService = cashClosingService;

        [HttpGet("preview")]
        public async Task<ActionResult<CashClosingDto>> GetPreview()
        {
            try
            {
               var result = await _cashClosingService.GetCurrentShiftInfoAsync();
               return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CashClosingDto>> PerformClosing(CreateCashClosingDto request)
        {
            try
            {
                var result = await _cashClosingService.PerformCashClosingAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
