using Microsoft.EntityFrameworkCore;
using Projet_5.Data;
using Projet_5.Models;

namespace Projet_5.Services
{
    /// <summary>
    /// Service permettant de gérer les réparations associées à des véhicules dans l'application.
    /// Ce service gère l'ajout, la mise à jour, la suppression et la récupération des réparations.
    /// </summary>
    public class RepairService : IRepairService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RepairService"/>.
        /// </summary>
        /// <param name="context">Le contexte de la base de données pour accéder aux réparations.</param>
        public RepairService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère une réparation par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de la réparation à récupérer.</param>
        /// <returns>La réparation correspondante.</returns>
        /// <exception cref="ArgumentException">Si l'identifiant est inférieur ou égal à 0.</exception>
        /// <exception cref="InvalidOperationException">Si aucune réparation n'est trouvée avec cet identifiant.</exception>
        public async Task<Repair> GetRepairByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Repair ID must be greater than 0.", nameof(id));
            }

            var repair = await _context.Set<Repair>()
                            .Where(r => r.Id == id)
                            .FirstOrDefaultAsync();

            if (repair == null)
            {
                throw new InvalidOperationException($"No repair found with ID {id}");
            }

            return repair;
        }

        /// <summary>
        /// Récupère toutes les réparations associées à une annonce.
        /// </summary>
        /// <param name="advertisementId">L'identifiant de l'annonce.</param>
        /// <returns>La liste des réparations associées à l'annonce spécifiée.</returns>
        /// <exception cref="ArgumentException">Si l'identifiant de l'annonce est inférieur ou égal à 0.</exception>
        public async Task<List<Repair>> GetRepairByAdvertisementAsync(int advertisementId)
        {
            if (advertisementId <= 0)
            {
                throw new ArgumentException("Advertisement ID must be greater than 0", nameof(advertisementId));
            }
            return await _context.Set<Repair>()
                .Include(r => r.Vehicle)
                .Where(r => r.Advertisements.Id == advertisementId)
                .ToListAsync();
        }

        /// <summary>
        /// Ajoute une nouvelle réparation à un véhicule.
        /// </summary>
        /// <param name="vehicleId">L'identifiant du véhicule auquel la réparation est associée.</param>
        /// <param name="repair">L'objet réparation à ajouter.</param>
        /// <returns>La réparation ajoutée.</returns>
        /// <exception cref="Exception">Si le véhicule ou l'annonce associée ne sont pas trouvés.</exception>
        public async Task<Repair> AddRepairAsync(int vehicleId, Repair repair)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Advertisements)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                throw new Exception("Vehicule non trouvé.");
            }

            if (vehicle.Advertisements == null)
            {
                throw new Exception("Annonce non trouvée");
            }

            repair.Advertisements = vehicle.Advertisements.FirstOrDefault();

            _context.Repairs.Add(repair);
            await _context.SaveChangesAsync();

            return repair;
        }

        /// <summary>
        /// Met à jour une réparation existante.
        /// </summary>
        /// <param name="repair">L'objet réparation contenant les nouvelles informations.</param>
        /// <returns> Un Task qui représente l'opération asynchrone.</returns>
        /// <exception cref="Exception">Si la réparation n'existe pas dans la base de données.</exception>
        public async Task UpdateRepairAsync(Repair repair)
        {
            var existingRepair = await _context.Repairs.FindAsync(repair.Id);

            if (existingRepair != null)
            {
                existingRepair.Reparation = repair.Reparation;
                existingRepair.Cost = repair.Cost;

                _context.Repairs.Update(existingRepair);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("La réparation n'existe pas dans la base de données.");
            }
        }

        /// <summary>
        /// Supprime une réparation à partir de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de la réparation à supprimer.</param>
        /// <returns>Un Task qui représente l'opération asynchrone.</returns>
        /// <exception cref="Exception">Si la réparation n'existe pas dans la base de données.</exception>
        public async Task DeleteRepairAsync(int id)
        {
            var repair = await _context.Repairs.FindAsync(id);
            if (repair != null)
            {
                _context.Repairs.Remove(repair);
                await _context.SaveChangesAsync(); 
            }
            else
            {
                throw new Exception("La réparation n'existe pas dans la base de données.");
            }
        }

        /// <summary>
        /// Récupère toutes les réparations associées à un véhicule spécifique.
        /// </summary>
        /// <param name="vehicleId">L'identifiant du véhicule pour lequel récupérer les réparations.</param>
        /// <returns>La liste des réparations associées à ce véhicule.</returns>
        public async Task<List<Repair>> GetRepairsByVehicleIdAsync(int vehicleId)
        {
            return await _context.Repairs
                                 .Where(r => r.VehicleId == vehicleId)
                                 .ToListAsync();
        }
    }
}
