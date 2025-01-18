using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Projet_5.Models;
using Projet_5.Services;

namespace Projet_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IVehicleService _vehicleService;
        private readonly IRepairService _repairService;
        private readonly ITransactionService _transactionService;

        public AdvertisementController(IAdvertisementService advertisementService,IVehicleService vehicleService, IRepairService repairService, ITransactionService transactionService)
        {
            _advertisementService = advertisementService;
            _vehicleService = vehicleService;
            _repairService = repairService;
            _transactionService = transactionService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var advertisements = await _advertisementService.GetAllAdvertisementsAsync();
            return Ok(advertisements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdvertisementById(int id)
        {
            var advertisement = await _advertisementService.GetAdvertisementByIdAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return Ok(advertisement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdvertisement(int id, [FromBody] Advertisement updatedAdvertisement)
        {
            if (id != updatedAdvertisement.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _advertisementService.UpdateAdvertisementAsync(updatedAdvertisement);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            var success = await _advertisementService.DeleteAdvertisementAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("SetDisponibility")]
        public async Task<IActionResult> SetDisponibility([FromForm] int vehicleId, [FromForm] bool disponibility)
        {
            try
            {
                await _advertisementService.SetDisponibilityAsync(vehicleId, disponibility);
                return RedirectToAction("Details", new { id = vehicleId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Details", "Details", new { id = vehicleId });
            }
        }

        [HttpDelete("archive")]
        public IActionResult ArchiveAdvertisement([FromQuery] bool selled)
        {
            _advertisementService.ArchiveAnnoucement(selled);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDescription(int id, [FromBody] string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return BadRequest();
            }

            var success = await _advertisementService.UpdateDescriptionAsync(id, description);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhoto(int id, [FromBody] string photo)
        {
            if (string.IsNullOrWhiteSpace(photo))
            {
                return BadRequest();
            }

            var success = await _advertisementService.UpdatePhotoAsync(id, photo);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var repair = await _repairService.GetRepairsByVehicleIdAsync(id);
            var buyingTransaction = await _transactionService.GetBuyingTransactionByVehicleIdAsync(id);
            var sellingPrice = await _advertisementService.CalculateSellingPriceAsync(id);
            var advertisement = await _advertisementService.GetAdvertisementByVehicleIdAsync(id);

            var model = new VehicleDetailsViewModel
            {
                Vehicle = vehicle,
                Repairs = repair,
                BuyingTransaction = buyingTransaction,
                SellingPrice = sellingPrice,
                Advertisement = advertisement
            };

            return View(model);
        }

        [HttpPost]
        [Route("SetSelled")]
        public async Task<IActionResult> SetSelled([FromForm] int vehicleId, [FromForm] bool selled)
        {
            try
            {
                await _advertisementService.SetSelledAsync(vehicleId, selled);
                return RedirectToAction("Details", new { id = vehicleId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Details", "Details", new { id = vehicleId });
            }
        }

        [HttpPost]
        [Route("CalculateSellingPrice")]
        public async Task<IActionResult> CalculateSellingPrice(int vehicleId, int advertisementId)
        {
            try
            {
                var sellingPrice = await _advertisementService.CalculateSellingPriceAsync(vehicleId);

                return RedirectToAction("Details", new { id = vehicleId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", new { id = vehicleId });
            }
        }
    }
}
