using Humanizer;
using Microsoft.EntityFrameworkCore;
using Projet_5.Data;
using Projet_5.Models;

namespace Projet_5.Services
{
    /// <summary>
    /// Service pour gérer les transactions liées aux véhicules dans le système.
    /// Fournit des méthodes pour obtenir, ajouter, mettre à jour et supprimer des transactions.
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance du service TransactionService.
        /// </summary>
        /// <param name="context">Contexte de base de données pour accéder aux données des transactions.</param>
        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère une transaction par son identifiant unique.
        /// </summary>
        /// <param name="id">L'identifiant unique de la transaction.</param>
        /// <returns>Une instance de la transaction si trouvée, sinon renvoi null.</returns>
        /// <exception cref="ArgumentNullException">Si l'identifiant est inférieur ou égal à zéro.</exception>
        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Id cannot be null or empty.", nameof(id));
            }
            else
            {
                var transaction = await _context.Transactions
                    .FirstOrDefaultAsync(t => t.Id == id);

                return transaction;
            }
        }

        /// <summary>
        /// Récupère la transaction d'achat liée à un véhicule.
        /// </summary>
        /// <param name="vehicleId">L'identifiant du véhicule pour lequel l'achat est effectué.</param>
        /// <returns>La transaction d'achat si trouvée, sinon renvoi null.</returns>
        /// <exception cref="ArgumentNullException">Si l'identifiant du véhicule est inférieur ou égal à zéro.</exception>
        public async Task<Transaction> GetBuyingTransactionByVehicleIdAsync(int vehicleId)
        {
            if (vehicleId <= 0)
            {
                throw new ArgumentNullException("Id cannot be null or empty.", nameof(vehicleId));
            }
            else
            {
                var transaction = await _context.Transactions
                    .Include(t => t.Vehicle)
                    .FirstOrDefaultAsync(t => t.VehicleId == vehicleId && t.Type == false);

                return transaction;
            }
        }

        /// <summary>
        /// Récupère la transaction de vente liée à un véhicule.
        /// </summary>
        /// <param name="vehicleId">L'identifiant du véhicule pour lequel la vente est effectuée.</param>
        /// <returns>La transaction de vente si trouvée, sinon renvoi null.</returns>
        /// <exception cref="ArgumentNullException">Si l'identifiant du véhicule est inférieur ou égal à zéro.</exception>
        public async Task<Transaction> GetSellingTransactionByVehicleIdAsync(int vehicleId)
        {
            if (vehicleId <= 0)
            {
                throw new ArgumentNullException("Id cannot be null or empty.", nameof(vehicleId));
            }
            else
            {
                var transaction = await _context.Transactions
                    .Include(t => t.Vehicle)
                    .FirstOrDefaultAsync(t => t.VehicleId == vehicleId && t.Type == true);

                return transaction;
            }
        }

        /// <summary>
        /// Récupère toutes les transactions enregistrées dans le système.
        /// </summary>
        /// <returns>Une liste de toutes les transactions.</returns>
        public async Task<List<Transaction>> GetAllTransactionAsync()
        {
            return await _context.Set<Transaction>()
                .Include(t => t.Vehicle)
                .ToListAsync();
        }

        /// <summary>
        /// Ajoute une nouvelle transaction dans la base de données.
        /// </summary>
        /// <param name="amount">Le montant de la transaction.</param>
        /// <param name="vehicleId">L'identifiant du véhicule concerné par la transaction.</param>
        /// <param name="advertisementId">L'identifiant de l'annonce associée à la transaction.</param>
        /// <param name="type">Le type de la transaction (achat ou vente).</param>
        /// <returns>La transaction nouvellement ajoutée.</returns>
        public async Task<Transaction> AddTransactionAsync(float amount, int vehicleId, int advertisementId, bool type)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                Type = type,
                TransactionDate = DateTime.Now,
                VehicleId = vehicleId,
                AdvertisementId = advertisementId
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        /// <summary>
        /// Met à jour une transaction existante dans la base de données.
        /// </summary>
        /// <param name="transaction">L'objet transaction contenant les données mises à jour.</param>
        /// <param name="id">L'identifiant de la transaction à mettre à jour.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateTransactionAsync(Transaction transaction, int id)
        {
            var existingTransaction = await GetTransactionByIdAsync(id);

            if (existingTransaction == null)
            {
                return false;
            }
            else
            {
                existingTransaction.Id = transaction.Id;
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.Type = transaction.Type;
                existingTransaction.TransactionDate = transaction.TransactionDate;

                await _context.SaveChangesAsync();

                return true;
            }
        }

        /// <summary>
        /// Supprime une transaction existante.
        /// </summary>
        /// <param name="id">L'identifiant de la transaction à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return false;
            }
            else
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();

                return true;
            }
        }
        
    }
    
}
