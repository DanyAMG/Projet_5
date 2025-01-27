using Projet_5.Models;

namespace Projet_5.Services
{
    /// <summary>
    /// Interface pour la gestion des réparations dans le système.
    /// </summary>
    public interface IRepairService
    {
        /// <summary>
        /// Récupère une réparation par son Id.
        /// </summary>
        /// <param name="id">L'Id de la réparation à rechercher.</param>
        /// <returns>La réparation correspondante.</returns>
        Task<Repair> GetRepairByIdAsync(int id);

        /// <summary>
        /// Récupère toutes les réparations associées à une annonce.
        /// </summary>
        /// <param name="advertisementId">L'Id de l'annonce.</param>
        /// <returns>Une liste de réparations associées à l'annonce.</returns>
        Task<List<Repair>> GetRepairByAdvertisementAsync(int advertisementId);

        /// <summary>
        /// Ajoute une nouvelle réparation pour un véhicule spécifique.
        /// </summary>
        /// <param name="vehicleId">L'Id du véhicule auquel la réparation est associée.</param>
        /// <param name="repair">L'objet Repair contenant les détails de la réparation.</param>
        /// <returns>La réparation ajoutée.</returns>
        Task<Repair> AddRepairAsync(int vehicleId, Repair repair);

        /// <summary>
        /// Met à jour une réparation existante.
        /// </summary>
        /// <param name="repair">L'objet <see cref="Repair"/> contenant les nouvelles informations.</param>
        /// <returns>True si la mise à jour a réussi, False si elle a échoué.</returns>
        Task UpdateRepairAsync(Repair repair);

        /// <summary>
        /// Supprime une réparation par son Id.
        /// </summary>
        /// <param name="id">L'Id de la réparation à supprimer.</param>
        /// <returns>True si la suppression a réussi, False si elle a échoué.</returns>
        Task DeleteRepairAsync(int id);

        /// <summary>
        /// Récupère toutes les réparations associées à un véhicule.
        /// </summary>
        /// <param name="vehicleId">L'Id du véhicule.</param>
        /// <returns>Une liste de réparations associées au véhicule.</returns>
        Task<List<Repair>> GetRepairsByVehicleIdAsync(int vehicleId);
    }
}