using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Projet_5.Areas.Identity.Data;

namespace Projet_5.Controllers
{
    /// <summary>
    /// Contrôleur pour gérer les opérations liées aux véhicules, telle que l'ajout, la mise à jour, la supression, la recherche et l'affichage des détails.
    /// </summary>
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ITransactionService _transactionService;
        private readonly IRepairService _repairService;
        private readonly IAdvertisementService _advertisementService;

        /// <summary>
        /// Constructeur injectant les dépendances pour gérer les véhicules, transactions, réparations et annonces.
        /// </summary>
        /// <param name="vehicleService"> Service pour gérer les opérations liées aux véhicules. </param>
        /// <param name="transactionService"> Service pour gérer les transactions liées aux véhicules</param>
        /// <param name="repairService"> Service pour gérer les réparations liées aux véhicules </param>
        /// <param name="advertisementService"> Service pour gérer les annonces liées aux véhicules. </param>
        public VehicleController(IVehicleService vehicleService, ITransactionService transactionService, IRepairService repairService, IAdvertisementService advertisementService)
        {
            _vehicleService = vehicleService;
            _transactionService = transactionService;
            _repairService = repairService;
            _advertisementService = advertisementService;
        }

        /// <summary>
        /// Affiche le formulaire pour ajouter un véhicule. Accéssible uniquement aux compte identifié en tant qu'administrateur.
        /// </summary>
        /// <returns> Retourne une vue conentant le formulaire d'ajout de véhicule. </returns>
        [Authorize(Roles = "Admin")]
        public IActionResult AddVehicle()
        {
            var model = new VehicleViewModel();
            return View(model);
        }

        /// <summary>
        ///  Récupère tous les véhicules enregistrés dans la base de données.
        /// </summary>
        /// <returns> Retourne un objet JSON contenant la liste des véhicules avec le statut code HTTP 200  </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var  vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        /// <summary>
        /// Récupère le véhicule dont l'Id correspond à l'Id renseigné
        /// </summary>
        /// <param name="id"> L'Id du véhicule que l'on recherche</param>
        /// <returns> Retourne un objet JSON contenant les informations de véhicule correspondant avec le statut code HTTP 200
        /// Si le véhicule correspondant à l'Id n'existe pas, retourne une réponse HTTP 404
        /// </returns>.
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

        /// <summary>
        /// Ajoute un nouveau véhicule à la base de données. Crée également une annonce et une transaction associées.
        /// </summary>
        /// <param name="model"> Le modèle de vue contenant les informations pour ajouter un véhicule.</param>
        /// <returns> Si l'ajout est réussi redirige vers la vue de confirmant l'ajout du véhicule.
        /// Sinon retourne le formulaire en indiquant les erreurs de validation.
        /// </returns>
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

            //Création de l'objet Vehicle avec les informations fournit par le modèle
            var vehicle = new Vehicle
            {
                VIN = model.VIN,
                Brand = model.Brand,
                Model = model.VehiculeModel,
                Year = model.Year,
                Finition = model.Finition
            };

            //Gestion de l'upload du fichier photo
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

            // Ajout du véhicule dans la base de données
            await _vehicleService.AddVehicleAsync(vehicle);

            //Création et ajout d'une annonce associé dans la base de données
            var advertisement = new Advertisement
            {
                Disponibility = false,
                Selled = false,
                Description = "this is a vehicle",
            };
            await _advertisementService.AddAdvertisementAsync(advertisement, vehicle);

            // Création d'une variable boolénne en état Faux pour indiquer une transaction de type de Achat
            bool transactiontype = false;
            //Création et ajout d'une transaction associée
            await _transactionService.AddTransactionAsync(model.Price, vehicle.Id, advertisement.Id, transactiontype);

            return RedirectToAction("VehicleAdded");
        }

        /// <summary>
        /// Met à jour les informations d'un véhicule existant.
        /// </summary>
        /// <param name="id"> Id du véhicule à mettre à jour.</param>
        /// <param name="vehicle"> Le modèle de vue contenant les informations pour mettre à jour un véhicule.</param>
        /// <returns> Retourne un status code HTTP 400(Bad Request) si les informations du modèles sont invalides.
        /// Retourne un status code HTTP 404 (Not Found) si le véhicule n'est pas trouvé.
        /// Retourne un status code HTTP 204 (NoContent) si la mise à jour a été effectué.
        /// </returns>
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

        /// <summary>
        /// Supprime de la base de données un vhéicule existant.
        /// </summary>
        /// <param name="id"> Id du véhicule que l'on cherche à supprimer.</param>
        /// <returns> Retourne un status code HTTP 404 (Not Found) si le véhicule n'est pas trouvé.
        /// Redirige vers la page d'accueil si la suppresion est effectué.
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound("Le véhicule spécifié n'existe pas.");
            }

            //Suppression des réparations associées au véhicule
            var repairs = await _repairService.GetRepairsByVehicleIdAsync(id);
            foreach (var repair in repairs)
            {
                await _repairService.DeleteRepairAsync(repair.Id);
            }

            //Suppression des transactions associées au véhicule
            var buyingTransaction = await _transactionService.GetBuyingTransactionByVehicleIdAsync(id);
            var sellingTransaction = await _transactionService.GetSellingTransactionByVehicleIdAsync(id);

            if (buyingTransaction != null)
            {
                await _transactionService.DeleteTransactionAsync(buyingTransaction.Id);
            }

            if (sellingTransaction != null)
            {
                await _transactionService.DeleteTransactionAsync(sellingTransaction.Id);
            }

            //Suppression de l'annonce associé au véhicule
            var advertisement = await _advertisementService.GetAdvertisementByVehicleIdAsync(id);
            if (advertisement != null)
            {
                await _advertisementService.DeleteAdvertisementAsync(advertisement.Id);
            }

            //Supression du véhicule
            var result = await _vehicleService.DeleteVehicleAsync(id);
            if (!result)
            {
                ModelState.AddModelError("", "Erreur lors de la suppression du véhicule.");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Affiche la vue de confirmation d'ajout d'un véhicule.
        /// </summary>
        /// <returns> Retourne la vue de confirmation d'ajout d'un véhicule.</returns>
        public IActionResult VehicleAdded()
        {
            return View();
        }

        /// <summary>
        /// Affiche le formulaire d'édition d'un véhicule existant.
        /// </summary>
        /// <param name="id"> Id du véhicule que l'on veut modifier.</param>
        /// <returns> Retourne un status code HTTP 404 (Not Found) si le véhicule n'est pas trouvé.
        /// Retourne la vue de détails du véhicule.
        /// </returns>
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

        /// <summary>
        /// Met à jour les informations d'un véhicule
        /// </summary>
        /// <param name="model"> Le modèle de vue contenant les informations pour ajouter un véhicule.</param>
        /// <param name="id"> L'Id du véhicule à metre à jour.</param>
        /// <returns> Retourne la vue avec les erreurs en cas de formulaire invalide.
        /// Retourne la vue de détails du véhicule une fois la modification faite.
        /// </returns>
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

        /// <summary>
        /// Retourne vers une page pour confirmer la suppression d'un véhicule.
        /// </summary>
        /// <param name="id"> Id du véhicule à supprimer</param>
        /// <returns> Retourne vers la page de confirmation de suppression.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ConfirmDelete(int id)
        {
            return View(id);
        }
    }
}
