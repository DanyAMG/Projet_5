﻿using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Projet_5.Areas.Identity.Data;

namespace Projet_5.Controllers
{
    
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ITransactionService _transactionService;
        private readonly IRepairService _repairService;
        private readonly IAdvertisementService _advertisementService;
        public VehicleController(IVehicleService vehicleService, ITransactionService transactionService, IRepairService repairService, IAdvertisementService advertisementService)
        {
            _vehicleService = vehicleService;
            _transactionService = transactionService;
            _repairService = repairService;
            _advertisementService = advertisementService;
        }

        [Authorize(Roles = "Admin")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
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
        [Authorize(Roles = "Admin")]
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
                Model = model.VehiculeModel,
                Year = model.Year,
                Finition = model.Finition
            };

            if (model.Photo != null && model.Photo.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                vehicle.PhotoPath = "/uploads/" + fileName;
            }
            else
            {
                vehicle.PhotoPath = "/uploads/default.png";
            }

            await _vehicleService.AddVehicleAsync(vehicle);

            var advertisement = new Advertisement
            {
                Disponibility = false,
                Selled = false,
                Description = "this is a vehicle",
            };

            await _advertisementService.AddAdvertisementAsync(advertisement, vehicle);
            bool transactiontype = false;
            await _transactionService.AddTransactionAsync(model.Price, vehicle.Id, advertisement.Id, transactiontype);

            return RedirectToAction("VehicleAdded");
        }
        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid || id != vehicle.Id)
            {
                return BadRequest(ModelState);
            }

            var updated = await _vehicleService.UpdateVehicleAsync(id, vehicle);

            if(updated == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{vin}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var deleted = await _vehicleService.DeleteVehicleAsync(id);
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCar(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

            var advertisement = await _advertisementService.GetAdvertisementByVehicleIdAsync(id);

            var transaction = await _transactionService.GetBuyingTransactionByVehicleIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            if (advertisement == null)
            {
                return NotFound();
            }
            var model = new VehicleViewModel
            {
                VIN = vehicle.VIN,
                Brand = vehicle.Brand,
                VehiculeModel = vehicle.Model,
                Year = vehicle.Year,
                Finition = vehicle.Finition,
                Price = transaction.Amount
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCar(VehicleViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingVehicle = await _vehicleService.GetVehicleByIdAsync(id);
            var existingAdvertisement = await _advertisementService.GetAdvertisementByVehicleIdAsync(id);
            var existingTransaction = await _transactionService.GetBuyingTransactionByVehicleIdAsync(id);

            if (existingVehicle == null)
            {
                ModelState.AddModelError("", "Véhicule non trouvé.");
                return View(model);
            }

            if (existingAdvertisement == null)
            {
                ModelState.AddModelError("", "Annonce non trouvée.");
                return View(model);
            }

            existingVehicle.VIN = model.VIN;
            existingVehicle.Brand = model.Brand;
            existingVehicle.Model = model.VehiculeModel;
            existingVehicle.Year = model.Year;
            existingVehicle.Finition = model.Finition;

            if (model.Photo != null && model.Photo.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                existingVehicle.PhotoPath = "/uploads/" + fileName;
            }
            else
            {
                existingVehicle.PhotoPath = existingVehicle.PhotoPath;
            }
            var result = await _vehicleService.UpdateVehicleAsync(id, existingVehicle);

            if (!result)
            {
                ModelState.AddModelError("", "Erreur lors de la mise à jour du véhicule.");
                return View(model);
            }

            if (model.Price > 0)
            {
                existingTransaction.Amount = model.Price;
                existingTransaction.TransactionDate = DateTime.Now;
                await _transactionService.UpdateTransactionAsync(existingTransaction, existingTransaction.Id);
            }

            return RedirectToAction("Details", "Advertisement", new { id = id });
        }  
    }
}
