using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Models.Services;

namespace Projet_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet("{vin}")]
        public async Task<IActionResult> GetPurchaseByVin(string vin)
        {
            var purchase= await _purchaseService.GetPurchaseByVinAsync(vin);
            if (purchase == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(purchase);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPurchase([FromBody] Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                await _purchaseService.AddPurchaseAsync(purchase);
                return CreatedAtAction(nameof(GetPurchaseByVin), new { vin = purchase.VIN}, purchase);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePurchase([FromBody] Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updated = await _purchaseService.UpdatePurchaseAsync(purchase);

            if (updated == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
