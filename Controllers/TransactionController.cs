using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Projet_5.Models;
using Projet_5.Services;

namespace Projet_5.Controllers
{
    /// <summary>
    /// Controleur pour gérer les opérations liées aux transactions, tel que la création, suppression, recherche.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        /// <summary>
        /// Constructeur injectant les dépendances pour gérer les transactions.
        /// </summary>
        /// <param name="transactionService"> Service pour gérer les opérations liées aux transactions.</param>
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Récupere une transaction associé à un Id donné.
        /// </summary>
        /// <param name="id"> Identifiant de la transaction a recherché.</param>
        /// <returns> Retourne un objet JSON contenant la transaction enregistrée associée à l'Id avec le statut code HTTP200.
        /// Si la transaction correspondant à l'Id n'existe pas, retourne un statut code HTTP404(NotFound)
        /// </returns>
        [HttpGet("{vin}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(transaction);
            }
        }

        /// <summary>
        /// Récupère la liste de transaction enreigstrée dans la base de données.
        /// </summary>
        /// <returns>
        /// Retourne un objet JSON contenant la liste de transactions enregistrées associées à l'Id avec le statut code HTTP200.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTransaction()
        {
            var transactions = await _transactionService.GetAllTransactionAsync();
            return Ok(transactions);
        }

        /// <summary>
        /// Ajoute une transaction dans la base de données.
        /// </summary>
        /// <param name="transaction"> Instance de Transaction contenant les informations de la transaction à ajouter.</param>
        /// <returns> Retourne un statut code HTTP400(BadRequest)  si les informations du modèles sont invalides.
        /// Retourne un statut code HTTP201(Created) avec les informations de la transaction ajoutée.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdTransaction = await _transactionService.AddTransactionAsync(transaction.Amount, transaction.VehicleId, transaction.AdvertisementId, transaction.Type);

            return CreatedAtAction(nameof(GetTransactionById), new { id = createdTransaction.Id }, createdTransaction);
        }

        /// <summary>
        /// Met à jour une transaction donnée.
        /// </summary>
        /// <param name="id"> Identifiant de la transaction à mettre à jour.</param>
        /// <param name="updatedTransaction"> Instance de Transaction contenant les informations</param>
        /// <returns> Retourne un statut code HTTP400(BadRequest)  si les informations du modèles sont invalides ou si l'Id de la transaction est invalide..
        /// Retourne un status code HTTP 404 (Not Found) si lea transaction n'est pas trouvée.
        /// Retourne un status code HTTP 204 (NoContent) si la mise à jour a été effectué.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction updatedTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedTransaction.Id)
            {
                return BadRequest("L'ID de la transaction ne correspond pas.");
            }

            try
            {

                var success = await _transactionService.UpdateTransactionAsync(updatedTransaction, id);

                if (!success)
                {
                    return NotFound("Transaction introuvable.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }
    }
    }
