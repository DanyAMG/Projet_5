using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Models.Services;

namespace Projet_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SellController : ControllerBase
    {
        private readonly ISellService _sellService;

        public SellController(ISellService sellService)
        {
            _sellService = sellService;
        }

        [HttpGet("vin")]
        public async Task<IActionResult> GetSellByVin(string vin)
        {
            var sell = await _sellService.GetSellByVinAsync(vin);
            if (sell == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sell);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSell([FromBody] Sell sell)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                await _sellService.AddSellAsync(sell);
                return CreatedAtAction(nameof(GetSellByVin), new { vin = sell.VIN }, sell);
            }    
    }
        [HttpPut]
        public async Task<IActionResult> UpdateSell(Sell sell)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updated = await _sellService.UpdateSellAsync(sell);
            if (updated == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
