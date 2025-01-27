using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Services;
using System.Diagnostics;

namespace Projet_5.Controllers
{
    /// <summary>
    /// Controleur qui g�re les pages d'accueil.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleService _vehicleService;
        private readonly IAdvertisementService _advertisementService;

        /// <summary>
        /// Initialise une instance du contr�leur Home.
        /// </summary>
        /// <param name="logger"> Instance de Logger pour la journalisation.</param>
        /// <param name="vehicleService"> Service pour g�rer les donn�es des v�hicules.</param>
        /// <param name="advertisementService"> Service pour g�rer les annonces.</param>
        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService, IAdvertisementService advertisementService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
            _advertisementService = advertisementService;
        }

        /// <summary>
        /// Affiche la page d'accueil avec une liste de v�hicule disponible � la vente.
        /// Les utilisateurs connect�s en tant qu'Administrateur pourront voir tous les v�hicules enregistr�es dans la base de donn�es.
        /// </summary>
        /// <returns></returns>
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
