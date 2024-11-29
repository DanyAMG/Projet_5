using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Services;


namespace Projet_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RepairController : ControllerBase
    {
        private readonly IRepairService _repairService;

        public RepairController(IRepairService repairService)
        {
            _repairService = repairService;
        }

        [HttpGet("vin")]
        public async Task<IActionResult> GetRepairsByVin(string vin)
        {
            var repair = await _repairService.GetRepairByVinAsync(vin);
            if(repair == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(repair);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRepair(Repair repair)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updated = await _repairService.UpdateRepairAsync(repair);
            if (updated == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepaire(int id)
        {
            var deleted = await _repairService.RemoveRepairAsync(id);
            if(!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
