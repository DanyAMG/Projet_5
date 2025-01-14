using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_5.Data;
using Projet_5.Models;
using System;

namespace Projet_5.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Advertisement>> GetAllAdvertisementsAsync()
        {
            return await _context.Advertisements.ToListAsync();
        }

        public async Task<Advertisement> GetAdvertisementByIdAsync(int id)
        {
            return await _context.Advertisements.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Advertisement> GetAdvertisementByVehicleIdAsync(int vehicleId)
        {
            return await _context.Advertisements
                .FirstOrDefaultAsync(a => a.VehicleId == vehicleId);
        }

        public async Task<Advertisement> AddAdvertisementAsync(Advertisement advertisement, Vehicle vehicle)
        {
            advertisement.VehicleId = vehicle.Id;
            _context.Advertisements.Add(advertisement);
            await _context.SaveChangesAsync();
            return advertisement;
        }

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

        public void ArchiveAnnoucement(bool selled)
        {
            var advertisementsToArchive = _context.Set<Advertisement>().Where(a => !a.Selled).ToList();

            foreach (var advertisement in advertisementsToArchive)
            {
                advertisement.Selled = selled;
            }
            _context.SaveChanges();
        }

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

        
    }
}
