using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Projet_5.Models;
using Projet_5.Services;

namespace Projet_5.Controllers
{
    /// <summary>
    /// Controleur pour gérer les opérations liées aux annonces, tel que la création, la mise à jour, la recherche et la suppression des annonces.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IVehicleService _vehicleService;
        private readonly IRepairService _repairService;
        private readonly ITransactionService _transactionService;

        /// <summary>
        /// Constructeur injectant les dépendances pour gérer les annonces, véhicules, réparation et transaction.
        /// </summary>
        /// <param name="advertisementService"> Service pour gérer les opérations liées aux annonces.</param>
        /// <param name="vehicleService"> Service pour gérer les opérations liées aux véhicules.</param>
        /// <param name="repairService"> Service pour gérer les réparations liées aux réparations.</param>
        /// <param name="transactionService"> Service pour gérer les transactions liées aux transactions.</param>
        public AdvertisementController(IAdvertisementService advertisementService,IVehicleService vehicleService, IRepairService repairService, ITransactionService transactionService)
        {
            _advertisementService = advertisementService;
            _vehicleService = vehicleService;
            _repairService = repairService;
            _transactionService = transactionService;

        }

        /// <summary>
        /// Récupère toutes les annonces enregistrées dans la base de données.
        /// </summary>
        /// <returns> Retourne un objet JSON contenant la liste des annonces enregistrées avec le statut code HTTP200</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var advertisements = await _advertisementService.GetAllAdvertisementsAsync();
            return Ok(advertisements);
        }

        /// <summary>
        /// Récupère une annonce dont l'Id correspond à l'Id recherché. 
        /// </summary>
        /// <param name="id"> Id de l'annonce a recherché.</param>
        /// <returns>Retourne un objet JSON contenant les informations de l'annonce correspondante avec le statut code HTTP200
        /// Si l'annonce correspondant à l'Id n'existe pas, retourne une réponse HTTP404(NotFound)
        /// </returns>
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

        /// <summary>
        /// Met à jour les informations d'une annonce existante.
        /// </summary>
        /// <param name="id"> Id de l'annonce a mettre à jour.</param>
        /// <param name="updatedAdvertisement"> Le modèle de l'annonce contenant les informations pour mettre à jour l'annonce..</param>
        /// <returns> Retourne un status code HTTP 400(Bad Request) si les informations du modèles sont invalides.
        /// Retourne un status code HTTP 404 (Not Found) si l'annonce n'est pas trouvée.
        /// Retourne un status code HTTP 204 (NoContent) si la mise à jour a été effectué.
        /// </returns>
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

        /// <summary>
        /// Supprime une annonce existante
        /// </summary>
        /// <param name="id"> Id de l'annonce à supprimer.</param>
        /// <returns> Retourne un status code HTTP 404 (Not Found) si le annonce n'est pas trouvée.
        /// Retourne un status code HTTP 204 (NoContent) si la mise à jour a été effectué.
        /// </returns>
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

        /// <summary>
        /// Modifie l'état de la disponibilité du véhicule.
        /// </summary>
        /// <param name="vehicleId"> Id du véhicle dont il faut mettre à jour la disponibilité</param>
        /// <param name="disponibility"> Etat de la disponiblité du véhicule.</param>
        /// <returns> Redirige vers la vue de détails du véhicule.</returns>
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

        /// <summary>
        /// Met à jour la déscription de l'annonce.
        /// </summary>
        /// <param name="id"> Id de l'annonce dont il faut mettre à jour la déscription.</param>
        /// <param name="description"> Le modèle de vue contenant la déscription pour mettre à jour l'annonce.</param>
        /// <returns> Retourne un objet JSON avec le statut code 200 et les informations de la déscription.</returns>
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

        /// <summary>
        /// Affiche les détails d'un véhicule, y compris les réparations, les transactions associés et le prix de vente calculé.
        /// </summary>
        /// <param name="id"> Id du véhicule dont on veut afficher les annonces.</param>
        /// <returns> Retourne la vue contentant les détails du véhicule.
        /// Retourne une erreur 404(NotFound) si l'identifiant ne correspond pas à un véhicule existant.
        /// </returns>
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

        /// <summary>
        /// Modifie l'état de vente du véhicule.
        /// </summary>
        /// <param name="vehicleId"> Id du véhicule dont on veut modifier l'état de vente.</param>
        /// <param name="selled"> Boolen qui indique l'état de vente du véhicule.
        /// True étant un véhicule vendu.
        /// False étant un véhicule non vendu. </param>
        /// <returns> Redirige vers la vue de détails du véhicule.</returns>
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

        /// <summary>
        /// Calcule le prix de vente en faisant la somme du prix d'achat du véhicule, du cout des réparations et de la marge initiale de 500€.
        /// </summary>
        /// <param name="vehicleId"> Id du véhicule dont il faut recalculer le prix de vente.</param>
        /// <param name="advertisementId"> Id de l'annonce dont il faut mettre à jour le prix de vente.</param>
        /// <returns> Redirige vers la vue de détails du véhicule.</returns>
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
