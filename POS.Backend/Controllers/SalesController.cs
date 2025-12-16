using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Backend.Services;
using POS.Shared.DTOs.Sale;

namespace POS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController(ISaleService saleService) : ControllerBase
    {
        private readonly ISaleService _saleService = saleService;

        [HttpPost("report")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetReport([FromBody] SaleFilterDto filter)
        {
             try
            {
                var sales = await _saleService.GetSalesByFilterAsync(filter);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<SaleDto>> Create(CreateSaleDto request)
        {
            try
            {
                var sale = await _saleService.CreateSaleAsync(request);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
