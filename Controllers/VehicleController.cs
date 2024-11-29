using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Services;

namespace Projet_5.Controllers
{
    
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
            
        }
        public IActionResult AddVehicle()
        {
            var model = new VehicleViewModel();
            return View(model);
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
        public async Task<IActionResult> AddVehicle([FromForm]VehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }

                return View(model);
            }

            var vehicle = new Vehicle
            {
                VIN = model.VIN,
                Brand = model.Brand,
                Model = model.Model,
                Year = model.Year,
                Finition = model.Finition
            };

            await _vehicleService.AddVehicleAsync(vehicle);

            return RedirectToAction("VehicleAdded");
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

        public IActionResult VehicleAdded()
        {
            return View();
        }
    }
}
