using Projet_5.Models;

namespace Projet_5.Services
{
    /// <summary>
    /// Interface définissant les opérations sur les transactions.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Récupère une transaction par son Id.
        /// </summary>
        /// <param name="id">L'Id de la transaction à rechercher.</param>
        /// <returns>La transaction correspondant.</returns>
        Task<Transaction> GetTransactionByIdAsync(int id);

        /// <summary>
        /// Récupère la transaction d'achat associée à un véhicule.
        /// </summary>
        /// <param name="id">L'Id du véhicule.</param>
        /// <returns>La transaction d'achat.</returns>
        Task<Transaction> GetBuyingTransactionByVehicleIdAsync(int id);

        /// <summary>
        /// Récupère la transaction de vente associée à un véhicule.
        /// </summary>
        /// <param name="id">L'Id du véhicule.</param>
        /// <returns>La transaction de vente.</returns>
        Task<Transaction> GetSellingTransactionByVehicleIdAsync(int id);

        /// <summary>
        /// Récupère toutes les transactions enregistrées.
        /// </summary>
        /// <returns>Une liste de toutes les transactions.</returns>
        Task<List<Transaction>> GetAllTransactionAsync();

        /// <summary>
        /// Ajoute une nouvelle transaction.
        /// </summary>
        /// <param name="amount">Le montant de la transaction.</param>
        /// <param name="vehicleId">L'Id du véhicule associé à la transaction.</param>
        /// <param name="advertisementId">L'Id de l'annonce associée à la transaction.</param>
        /// <param name="type">Le type de transaction.</param>
        Task<Transaction> AddTransactionAsync(float amount, int vehicleId, int advertisementId, bool type);

        /// <summary>
        /// Met à jour une transaction existante.
        /// </summary>
        /// <param name="transaction">Les nouvelles données de la transaction.</param>
        /// <param name="id">L'Id de la transaction à mettre à jour.</param>
        /// <returns>True si la mise à jour a réussi, False si elle a échoué.</returns>
        Task<bool> UpdateTransactionAsync(Transaction transaction, int id);

        /// <summary>
        /// Supprime une transaction par son Id.
        /// </summary>
        /// <param name="id">L'Id de la transaction à supprimer.</param>
        /// <returns>True si la suppression a réussi, False si elle a échoué.</returns>
        Task<bool> DeleteTransactionAsync(int id);
    }
}
