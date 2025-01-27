using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Projet_5.Data;
using Projet_5.Models;

namespace Projet_5.Services
{
    /// <summary>
    /// Service permettant de gérer les véhicules dans le système, incluant la récupération, l'ajout, la mise à jour et la suppression des véhicules.
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance de la classe VehicleService.
        /// </summary>
        /// <param name="context">Le contexte de la base de données pour accéder aux données des véhicules.</param>
        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère un véhicule par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant unique du véhicule.</param>
        /// <returns>Une instance du véhicule avec ses transactions, annonces et réparations associées, ou renvoi null si non trouvé.</returns>
        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.Transactions)
                .Include(v => v.Advertisements)
                .Include(v => v.Repairs)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        /// <summary>
        /// Récupère tous les véhicules dans la base de données avec leurs transactions, annonces et réparations associées.
        /// </summary>
        /// <returns>Une liste de tous les véhicules.</returns>
        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Transactions)
                .Include(v => v.Advertisements)
                .Include(v => v.Repairs)
                .ToListAsync();
        }

        /// <summary>
        /// Ajoute un nouveau véhicule dans la base de données.
        /// </summary>
        /// <param name="vehicle">L'objet véhicule à ajouter.</param>
        /// <returns>Le véhicule ajouté avec ses données, après l'insertion dans la base.</returns>
        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        /// <summary>
        /// Met à jour les informations d'un véhicule existant.
        /// </summary>
        /// <param name="id">L'identifiant du véhicule à mettre à jour.</param>
        /// <param name="vehicle">L'objet véhicule contenant les nouvelles données.</param>
        /// <returns>True si la mise à jour a été effectuée, sinon false.</returns>
        public async Task<bool> UpdateVehicleAsync(int id, Vehicle vehicle)
        {
            var existingVehicle = await GetVehicleByIdAsync(id);

            if (existingVehicle == null)
            {
                return false;
            }
            else
            {
                existingVehicle.Year = vehicle.Year;
                existingVehicle.Brand = vehicle.Brand;
                existingVehicle.Model = vehicle.Model;
                existingVehicle.Finition = vehicle.Finition;

                await _context.SaveChangesAsync();

                return true;
            }

        }

        /// <summary>
        /// Supprime un véhicule de la base de données.
        /// </summary>
        /// <param name="id">L'identifiant du véhicule à supprimer.</param>
        /// <returns>True si la suppression a été effectuée, sinon False.</returns>
        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return false;
            }
            else
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();

                return true;
            }
        }

        /// <summary>
        /// Récupère un véhicule avec ses détails complets, y compris les annonces, les réparations et les transactions.
        /// </summary>
        /// <param name="id">L'identifiant unique du véhicule.</param>
        /// <returns>Une instance du véhicule avec ses détails complets ou renvoi null si non trouvé.</returns>
        public async Task<Vehicle> GetVehicleWithDetailsAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.Advertisements)
                .Include(v => v.Repairs)
                .Include(v => v.Transactions)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
