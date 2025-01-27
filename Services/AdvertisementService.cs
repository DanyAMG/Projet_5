using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_5.Data;
using Projet_5.Models;
using System;
using System.Transactions;

namespace Projet_5.Services
{
    /// <summary>
    /// Service qui permet de gérer les annonces liées aux véhicules.
    /// Ce service permet d'ajouter, de mettre à jour, de supprimer des annonces, de gérer leur disponibilité et de calculer le prix de vente des véhicules.
    /// </summary>
    public class AdvertisementService : IAdvertisementService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance de Advertisement/>.
        /// </summary>
        /// <param name="context">Le contexte de la base de données pour accéder aux annonces et véhicules.</param>
        public AdvertisementService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère toutes les annonces dans la base de données.
        /// </summary>
        /// <returns>La liste de toutes les annonces.</returns>
        public async Task<List<Advertisement>> GetAllAdvertisementsAsync()
        {
            return await _context.Advertisements.ToListAsync();
        }

        /// <summary>
        /// Récupère une annonce par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'annonce.</param>
        /// <returns>L'annonce correspondante.</returns>
        public async Task<Advertisement> GetAdvertisementByIdAsync(int id)
        {
            return await _context.Advertisements.FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <summary>
        /// Récupère l'annonce associée à un véhicule donné.
        /// </summary>
        /// <param name="vehicleId">L'identifiant du véhicule.</param>
        /// <returns>L'annonce associée au véhicule.</returns>
        public async Task<Advertisement> GetAdvertisementByVehicleIdAsync(int vehicleId)
        {
            return await _context.Advertisements
                .FirstOrDefaultAsync(a => a.VehicleId == vehicleId);
        }

        /// <summary>
        /// Ajoute une annonce pour un véhicule.
        /// </summary>
        /// <param name="advertisement">L'objet annonce à ajouter.</param>
        /// <param name="vehicle">Le véhicule auquel l'annonce est liée.</param>
        /// <returns>L'annonce ajoutée.</returns>
        public async Task<Advertisement> AddAdvertisementAsync(Advertisement advertisement, Vehicle vehicle)
        {
            advertisement.VehicleId = vehicle.Id;
            _context.Advertisements.Add(advertisement);
            await _context.SaveChangesAsync();
            return advertisement;
        }

        /// <summary>
        /// Met à jour une annonce existante.
        /// </summary>
        /// <param name="advertisement">L'objet annonce contenant les informations mises à jour.</param>
        /// <returns>Un booléen indiquant si la mise à jour a réussi.</returns>
        public async Task<bool> UpdateAdvertisementAsync(Advertisement advertisement)
        {
            var existingAdvertisement = await GetAdvertisementByIdAsync(advertisement.Id);
            if (existingAdvertisement == null)
            {
                return false;
            }

            existingAdvertisement.Description = advertisement.Description;
            existingAdvertisement.PhotoPath = advertisement.PhotoPath;
            existingAdvertisement.Disponibility = advertisement.Disponibility;
            existingAdvertisement.DisponibilityDate = advertisement.DisponibilityDate;
            existingAdvertisement.Selled = advertisement.Selled;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Supprime une annonce par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'annonce à supprimer.</param>
        /// <returns>Un booléen indiquant si la suppression a réussi.</returns>
        public async Task<bool> DeleteAdvertisementAsync(int id)
        {
            var advertisement = await GetAdvertisementByIdAsync(id);
            if (advertisement == null)
            {
                return false;
            }

            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Modifie la disponibilité d'une annonce et l'ajoute si elle n'existe pas.
        /// </summary>
        /// <param name="vehicleId">L'identifiant du véhicule associé à l'annonce.</param>
        /// <param name="disponibility">La disponibilité à définir (true ou false).</param>
        public async Task SetDisponibilityAsync(int vehicleId, bool disponibility)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Advertisements)
                .FirstOrDefaultAsync(v=> v.Id == vehicleId);

            if (vehicle == null)
            {
                throw new Exception("Le vehicule n'existe pas");
            }

            if (disponibility)
            {
                var existingAdvertisement = vehicle.Advertisements.FirstOrDefault();
                if (existingAdvertisement == null)
                {
                    var advertisement = new Advertisement
                    {
                        VehicleId = vehicle.Id,
                        Disponibility = true,
                        DisponibilityDate = DateTime.Now,
                    };
                    _context.Advertisements.Add(advertisement);
                }
                else
                {
                    existingAdvertisement.Disponibility = true;
                    existingAdvertisement.DisponibilityDate = DateTime.Now;
                    _context.Advertisements.Update(existingAdvertisement);
                }

            }
            else
            {
                var existingAdvertisement = vehicle.Advertisements.FirstOrDefault();
                if ( existingAdvertisement != null)
                {
                    existingAdvertisement.Disponibility = false;
                    _context.Advertisements.Update(existingAdvertisement );
                }
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Archive les annonces non vendues.
        /// </summary>
        /// <param name="selled">Indique si les annonces doivent être marquées comme vendues.</param>
        public void ArchiveAnnoucement(bool selled)
        {
            var advertisementsToArchive = _context.Set<Advertisement>().Where(a => !a.Selled).ToList();

            foreach (var advertisement in advertisementsToArchive)
            {
                advertisement.Selled = selled;
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Met à jour la description d'une annonce.
        /// </summary>
        /// <param name="advertisementId">L'identifiant de l'annonce.</param>
        /// <param name="description">La nouvelle description à définir.</param>
        /// <returns>Un booléen indiquant si la mise à jour a réussi.</returns>
        public async Task<bool> UpdateDescriptionAsync(int advertisementId, string description)
        {
            var advertisement = await _context.Set<Advertisement>().FirstOrDefaultAsync(a => a.Id == advertisementId);

            if (advertisement == null)
            {
                return false;
            }

            advertisement.Description = description;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Met à jour la photo d'une annonce.
        /// </summary>
        /// <param name="advertisementId">L'identifiant de l'annonce.</param>
        /// <param name="photo">Le chemin vers la nouvelle photo à définir.</param>
        /// <returns>Un booléen indiquant si la mise à jour a réussi.</returns>
        public async Task<bool> UpdatePhotoAsync(int advertisementId, string photo)
        {
            var advertisement = await _context.Set<Advertisement>().FirstOrDefaultAsync(a => a.Id == advertisementId);

            if (advertisement == null)
            {
                return false;
            }

            advertisement.PhotoPath = photo;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Calcule le prix de vente d'un véhicule en fonction de son prix d'achat et des coûts des réparations.
        /// </summary>
        /// <param name="vehicleId">L'identifiant du véhicule pour lequel calculer le prix de vente.</param>
        /// <returns>Le prix de vente calculé.</returns>
        public async Task<float> CalculateSellingPriceAsync(int vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Transactions)
                .Include(v => v.Repairs)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                throw new Exception("Le vehicule n'existe pas");
            }

            var purchaseTransaction = vehicle.Transactions.FirstOrDefault(t => t.Type == false);
            if (purchaseTransaction == null)
            {
                throw new Exception("Aucune transaction trouvée pour ce véhicule");
            }

            var totalRepairCost = vehicle.Repairs.Sum(r => r.Cost);

            var sellingPrice = purchaseTransaction.Amount + totalRepairCost + 500;

            return sellingPrice;
        }

        /// <summary>
        /// Marque un véhicule comme vendu et crée une transaction correspondante.
        /// </summary>
        /// <param name="vehicleId">L'identifiant du véhicule à marquer comme vendu.</param>
        /// <param name="selled">Indique si le véhicule est vendu.</param>
        public async Task SetSelledAsync(int vehicleId, bool selled)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Advertisements)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                throw new Exception("Le véhicule n'existe pas");
            }
            var existingAdvertisement = vehicle.Advertisements.FirstOrDefault();
            if (existingAdvertisement.Disponibility == false)
            {
                throw new Exception("Le véhicule n'est pas encore disponible à la vente");
            }
            else
            {
                if (selled)
                {
                        existingAdvertisement.Selled = true;
                        _context.Advertisements.Update(existingAdvertisement);
                        float sellingPrice = await CalculateSellingPriceAsync(vehicleId);

                    var transaction = new Models.Transaction
                    {
                        Amount = sellingPrice,
                        VehicleId = vehicleId,
                        AdvertisementId = existingAdvertisement.Id,
                        Type = true,
                        TransactionDate = DateTime.Now
                    };
                    _context.Transactions.Add(transaction);
                }
                else
                {
                    existingAdvertisement.Selled = false;
                    _context.Advertisements.Update(existingAdvertisement);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
