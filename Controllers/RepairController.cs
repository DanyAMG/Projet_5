using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Projet_5.Controllers
{
    [Route("[controller]")]

    public class RepairController : Controller
    {
        private readonly IRepairService _repairService;
        private readonly IVehicleService _vehicleService;
        private readonly IAdvertisementService _advertisementService;
        public RepairController(IRepairService repairService, IVehicleService vehicleService, IAdvertisementService advertisementService)
        {
            _repairService = repairService;
            _vehicleService = vehicleService;
            _advertisementService = advertisementService;
        }

        [HttpGet("vin")]
        public async Task<IActionResult> GetRepairsByIdAsync(int id)
        {
            var repair = await _repairService.GetRepairByIdAsync(id);
            if(repair == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(repair);
            }
        }

        [HttpPost("/Repair/UpdateRepair")]
        public async Task<IActionResult> UpdateRepair(Repair repair)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _repairService.UpdateRepairAsync(repair);
            
            return NoContent();
        }

        //[HttpDelete("{id}")]
        [HttpPost("/Repair/DeleteRepair")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRepaire(int repairId)
        {
            var repair = await _repairService.GetRepairByIdAsync(repairId);

            if (repair == null)
            {
                return NotFound();
            }

            var vehicleId = repair.VehicleId;

            await _repairService.RemoveRepairAsync(repairId);

            return RedirectToAction("Details", "Advertisement", new { id = vehicleId });
        }

        [HttpGet("/Repair/AddRepair")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRepair(int vehicleId)
        {
            if (vehicleId <= 0)
            {
                return BadRequest("ID de véhicule invalide.");
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);

            if (vehicle == null)
            {
                return NotFound("Véhicule introuvable.");
            }

            var model = new RepairViewModel { VehicleId = vehicleId };

            return View(model);
        }

        [HttpPost("/Repair/AddRepair")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRepair(RepairViewModel model, int vehicleId)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(model.VehicleId);

            if (vehicle == null)
            {
                ModelState.AddModelError("", "Le véhicule spécifié n'existe pas.");
                return View(model);
            }

            var repair = new Repair
            {
                Reparation = model.Reparation,
                Cost = model.Cost,
                VehicleId = model.VehicleId
            };

            await _repairService.AddRepairAsync(vehicleId, repair);

            return RedirectToAction("RepairAdded", new { vehicleId = model.VehicleId });
        }

        [HttpGet("/Repair/RepairAdded")]
        [Authorize(Roles = "Admin")]
        public IActionResult RepairAdded(int vehicleId)
        {
            if (vehicleId <= 0)
            {
                return BadRequest("ID de véhicule invalide.");
            }

            var vehicle = _vehicleService.GetVehicleByIdAsync(vehicleId);

            if (vehicle == null)
            {
                return NotFound("Véhicule introuvable.");
            }

            ViewBag.VehicleId = vehicleId;

            return View(vehicle);
        }

        [HttpGet("/Repair/Edit")]
        public async Task<IActionResult> EditRepair(int id)
        {
            var repair = await _repairService.GetRepairByIdAsync(id);
            if (repair == null)
            {
                return NotFound();
            }
            return View(repair);
        }

        [HttpPost("/Repair/Edit")]
        public async Task<IActionResult> EditRepair(Repair repair)
        {
            if (repair.VehicleId <= 0 || repair.VehicleId <=0)
            {
                return BadRequest("L'ID du véhicule est invalide.");
            }
            await _repairService.UpdateRepairAsync(repair);

            return RedirectToAction("Details", "Advertisement", new { id = repair.VehicleId });
        }

        
    }
}
