using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Projet_5.Data;

namespace Projet_5.Models.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetVehicleByVinAsync(string vin)
        {
            return await _context.Vehicles
                .Include(v => v.Purchase)
                .Include(v => v.Sell)
                .Include(v => v.Repairs)
                .Include(v => v.IsAvailable)
                .FirstOrDefaultAsync(v => v.VIN == vin);
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Purchase)
                .Include(v => v.Sell)
                .Include(v => v.Repairs)
                .Include(v => v.IsAvailable)
                .ToListAsync();
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> UpdateVehicleAsync(string vin, Vehicle vehicle)
        {
            var existingVehicle = await GetVehicleByVinAsync(vin);

            if (existingVehicle == null)
            {
                return null;
            }
            else
            {
                existingVehicle.Year = vehicle.Year;
                existingVehicle.Brand = vehicle.Brand;
                existingVehicle.Model = vehicle.Model;
                existingVehicle.Finition = vehicle.Finition;
            }
            await _context.SaveChangesAsync();

            return existingVehicle;
        }

        public async Task<bool> DeleteVehicleAsync(string vin)
        {
            var vehicle = await GetVehicleByVinAsync(vin);
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

        public async Task<bool> MarkVehicleAsAvailable(string vin)
        {
            var vehicle = await GetVehicleByVinAsync(vin);

            if (vehicle == null)
            {
                return false;
            }
            else
            {
                vehicle.IsAvailable = true;
            }

            _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> MarkVehicleAsUnavailable(string vin)
        {
            var vehicle = await GetVehicleByVinAsync(vin);

            if (vehicle == null)
            {
                return false;
            }
            else
            {
                vehicle.IsAvailable = false;
            }

            _context.SaveChangesAsync();
            return true;
        }
    }
}
