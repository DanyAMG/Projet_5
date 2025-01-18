using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Services;
using System.Diagnostics;

namespace Projet_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IVehicleService _vehicleService;
        private readonly IAdvertisementService _advertisementService;

        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService, IAdvertisementService advertisementService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();

            if(!User.IsInRole("Admin"))
            {
                vehicles = vehicles.Where(v=> v.Advertisements.Any(a => a.Disponibility && !a.Selled)).ToList();
            }
            var vehicleViewModels = new List<VehicleViewModel>();

            
            return View(vehicles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
