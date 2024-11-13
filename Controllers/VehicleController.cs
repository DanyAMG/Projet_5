using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Models.Services;

namespace Projet_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var  vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{vin}")]
        public async Task<IActionResult> GetVehicleByVin(string vin)
        {
            var vehicle = await _vehicleService.GetVehicleByVinAsync(vin);
            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicle);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                await _vehicleService.AddVehicleAsync(vehicle);
                return CreatedAtAction(nameof(GetVehicleByVin), new { vin = vehicle.VIN},vehicle);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVehicle(string vin, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid || vin != vehicle.VIN)
            {
                return BadRequest(ModelState);
            }

            var updated = await _vehicleService.UpdateVehicleAsync(vin, vehicle);

            if(updated == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{vin}")]
        public async Task<IActionResult> DeleteVehicle(string vin)
        {
            var deleted = await _vehicleService.DeleteVehicleAsync(vin);
            if(!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPatch("{vin}/available")]
        public async Task<IActionResult> MarkVehicleAsAvailable(string vin)
        {
            var marked = await _vehicleService.MarkVehicleAsAvailable(vin);
            if(!marked)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPatch("{vin}/available")]
        public async Task<IActionResult> MarkVehicleAsUnavailable(string vin)
        {
            var unmarked = await _vehicleService.MarkVehicleAsUnavailable(vin);
            if (!unmarked)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
