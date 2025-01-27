using Projet_5.Models;

namespace Projet_5.Services
{
    /// <summary>
    /// Interface définissant les opérations sur les véhicules.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Récupère un véhicule par son Id.
        /// </summary>
        /// <param name="id">L'Id du véhicule à rechercher.</param>
        /// <returns>Le véhicule correspondant</returns>
        Task<Vehicle> GetVehicleByIdAsync(int id);

        /// <summary>
        /// Récupère la liste de tous les véhicules.
        /// </summary>
        /// <returns>Une liste de tous les véhicules enregistrés.</returns>
        Task<List<Vehicle>> GetAllVehiclesAsync();

        /// <summary>
        /// Ajoute un nouveau véhicule à la base de données.
        /// </summary>
        /// <param name="vehicle">Le véhicule à ajouter.</param>
        /// <returns>Le véhicule ajouté avec son Id généré.</returns>
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Met à jour les informations d'un véhicule existant.
        /// </summary>
        /// <param name="id">L'Id du véhicule à mettre à jour.</param>
        /// <param name="vehicle">Les nouvelles informations du véhicule.</param>
        /// <returns>True si la mise à jour a réussi, False si la mise à jour n'a pas été éffectué.</returns>
        Task<bool> UpdateVehicleAsync(int id, Vehicle vehicle);

        /// <summary>
        /// Supprime un véhicule par son Id.
        /// </summary>
        /// <param name="id">L'Id du véhicule à supprimer.</param>
        /// <returns>True si la suppression a réussi, False si la suppression a echoué.</returns>
        Task<bool> DeleteVehicleAsync(int id);

        /// <summary>
        /// Récupère un véhicule avec ses détails associés (réparations, annonces, etc.).
        /// </summary>
        /// <param name="id">L'Id du véhicule à récupérer.</param>
        /// <returns>Un véhicule avec tous ses détails.</returns>
        Task<Vehicle> GetVehicleWithDetailsAsync(int id);
    }
}
