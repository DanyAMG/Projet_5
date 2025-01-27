using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Projet_5.Controllers
{
    /// <summary>
    /// Controleur pour gérer les opérations liées aux réparations, tel que la création, suppression et la recherche.
    /// </summary>
    [Route("[controller]")]
    public class RepairController : Controller
    {
        private readonly IRepairService _repairService;
        private readonly IVehicleService _vehicleService;
        private readonly IAdvertisementService _advertisementService;

        /// <summary>
        /// Constructeur injectant les dépendances pour gérer les réparations, véhicules et annonces.
        /// </summary>
        /// <param name="repairService"> Service pour gérer les opérations liées aux réparations.</param>
        /// <param name="vehicleService"> Service pour gérer les opérations liées aux véhicules.</param>
        /// <param name="advertisementService"> Service pour gérer les opérations liées aux annonces.</param>
        public RepairController(IRepairService repairService, IVehicleService vehicleService, IAdvertisementService advertisementService)
        {
            _repairService = repairService;
            _vehicleService = vehicleService;
            _advertisementService = advertisementService;
        }

        /// <summary>
        /// Récupère la récupération associée à un Id.
        /// </summary>
        /// <param name="id"> Id de l'annonce recherchée.</param>
        /// <returns> Retourne un objet JSON contenant la réparation enregistrée associée à l'Id avec le statut code HTTP200
        /// Si la réparation correspondant à l'Id n'existe pas, retourne un statut code HTTP404(NotFound)
        /// </returns>
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

        /// <summary>
        /// Met à jour une réparation.
        /// </summary>
        /// <param name="repair"> Instance de Repair contenant les détails de la réparation.</param>
        /// <returns> Retourne un status code HTTP 400(Bad Request) si les informations du modèles sont invalides.
        /// Retourne un status code HTTP 204 (NoContent) si la mise à jour a été effectué.
        /// </returns>
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

        /// <summary>
        /// Supprime une réparation.
        /// </summary>
        /// <param name="repairId"> Id de la réparation a supprimé.</param>
        /// <returns>
        /// Redirige vers la page détail du véhicule concerné une fois la suppression effectué.
        /// Si la réparation correspondant à l'Id n'existe pas, retourne une réponse HTTP404(NotFound)
        /// </returns>
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

            await _repairService.DeleteRepairAsync(repairId);

            return RedirectToAction("Details", "Advertisement", new { id = vehicleId });
        }

        /// <summary>
        /// Affiche le formulaire d'ajout de réparation.
        /// </summary>
        /// <param name="vehicleId"> Id du véhicule auquel il faut rajouter une réparation.</param>
        /// <returns>
        /// Retourne la vue contenant le formulaire d'ajout de réparation.
        /// Retourne un status code HTTP400(Bad Request) si l'Id du véhicule recherché est invalide.
        /// Retourne un status code HTTTP404(NotFound) si le véhicule n'est pas trouvé dans la base de données.
        /// </returns>
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

        /// <summary>
        /// Ajoute une réparation à un véhicule.
        /// </summary>
        /// <param name="model"> Le modèle de vue contenant les informations de la réparation à ajouter.</param>
        /// <param name="vehicleId"> Id du véhicule auquel il faut rajouter une réparation.</param>
        /// <returns> Redirige vers une vue de confirmation d'ajout quand une réparation est ajoutée.
        /// Retourne le formumlaire avec les erreurs indiqués sur les champs érronné.
        /// </returns>
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

        /// <summary>
        /// Affiche une vue de confirmation d'ajout de réparation.
        /// </summary>
        /// <param name="vehicleId"> Id du véhicle auquel la réparation a été associé.</param>
        /// <returns> Retourne la vue de détails du véhicule.
        /// Retourne un status code HTTP400(Bad Request) si l'Id du véhicule recherché est invalide.
        /// Retourne un status code HTTTP404(NotFound) si le véhicule n'est pas trouvé dans la base de données.
        /// </returns>
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

        /// <summary>
        /// Affiche le formulaire de modification de réparation.
        /// </summary>
        /// <param name="id"> Identifiant de la réparation a modifié.</param>
        /// <returns>
        /// Retourne le formulaire d'édition de la réparation.
        /// Retourne un status code HTTP 404 (Not Found) si la réparation n'est pas trouvée.
        /// </returns>
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

        /// <summary>
        /// Met à jours les informations d'une réparation.
        /// </summary>
        /// <param name="repair"> Instance de la réparation à mettre à jour.</param>
        /// <returns>  Retourne un status code HTTP 400(Bad Request) si l'Id du véhicule associé est invalide.
        /// Redirige vers la vue de détails du véhicule.
        /// </returns>
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
