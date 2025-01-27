using Projet_5.Models;

namespace Projet_5.Services
{
    /// <summary>
    /// Interface pour la gestion des annonces publicitaires des véhicules.
    /// </summary>
    public interface IAdvertisementService
    {
        /// <summary>
        /// Récupère toutes les annonces.
        /// </summary>
        /// <returns>Une liste contenant toutes les annonces.</returns>
        Task<List<Advertisement>> GetAllAdvertisementsAsync();

        /// <summary>
        /// Récupère une annonce par son Id.
        /// </summary>
        /// <param name="id">L'Id de l'annonce à récupérer.</param>
        /// <returns>L'annonce correspondante.</returns>
        Task<Advertisement> GetAdvertisementByIdAsync(int id);

        /// <summary>
        /// Récupère une annonce associée à un véhicule.
        /// </summary>
        /// <param name="vehicleId">L'Id du véhicule.</param>
        /// <returns>L'annonce associée.</returns>
        Task<Advertisement> GetAdvertisementByVehicleIdAsync(int vehicleId);

        /// <summary>
        /// Ajoute une nouvelle annonce pour un véhicule.
        /// </summary>
        /// <param name="advertisement">L'annonce à ajouter.</param>
        /// <param name="vehicle">Le véhicule associé à l'annonce.</param>
        /// <returns>L'annonce ajoutée.</returns>
        Task<Advertisement> AddAdvertisementAsync(Advertisement advertisement, Vehicle vehicle);

        /// <summary>
        /// Met à jour les informations d'une annonce existante.
        /// </summary>
        /// <param name="advertisement">L'annonce contenant les nouvelles informations.</param>
        /// <returns>True si la mise à jour a réussi, False si elle a échoué.</returns>
        Task<bool> UpdateAdvertisementAsync(Advertisement advertisement);

        /// <summary>
        /// Supprime une annonce par son Id.
        /// </summary>
        /// <param name="id">L'Id de l'annonce à supprimer.</param>
        /// <returns>True si la suppression a réussi, False si elle a échoué.</returns>
        Task<bool> DeleteAdvertisementAsync(int id);

        /// <summary>
        /// Modifie la disponibilité d'une annonce associée à un véhicule.
        /// </summary>
        /// <param name="vehicleId">L'Id du véhicule.</param>
        /// <param name="disponibility">La nouvelle disponibilité de l'annonce.</param>
        /// <returns>Une tâche asynchrone.</returns>
        Task SetDisponibilityAsync(int vehicleId, bool disponibility);

        /// <summary>
        /// Calcule le prix de vente d'un véhicule en faisant la somme de son prix d'achat, du cout de ses réparations et de la marge de vente.
        /// </summary>
        /// <param name="vehicleId">L'Id du véhicule.</param>
        /// <returns>Le prix de vente calculé.</returns>
        Task<float> CalculateSellingPriceAsync(int vehicleId);

        /// <summary>
        /// Modifie l'état de vente d'une annonce associée à un véhicule.
        /// </summary>
        /// <param name="vehicleId">L'Id du véhicule.</param>
        /// <param name="selled">Indique si le véhicule a été vendu.</param>
        /// <returns>Une tâche asynchrone.</returns>
        Task SetSelledAsync(int vehicleId, bool selled);

        /// <summary>
        /// Archive les annonces marquées comme vendues.
        /// </summary>
        /// <param name="selled">Indique les annonces vendues qui doivent être archivées.</param>
        void ArchiveAnnoucement(bool selled);

        /// <summary>
        /// Met à jour la description d'une annonce.
        /// </summary>
        /// <param name="advertisementId">L'Id de l'annonce à mettre à jour.</param>
        /// <param name="description">La nouvelle description.</param>
        /// <returns>True si la mise à jour a réussi, False si elle a échoué.</returns>
        Task<bool> UpdateDescriptionAsync(int advertisementId, string description);

        /// <summary>
        /// Met à jour la photo associée à une annonce.
        /// </summary>
        /// <param name="advertisementId">L'Id de l'annonce à mettre à jour.</param>
        /// <param name="photo">Le chemin ou URL de la nouvelle photo.</param>
        /// <returns>True si la mise à jour a réussi, False si elle a échoué.</returns>
        Task<bool> UpdatePhotoAsync(int advertisementId, string photo);
    }
}
