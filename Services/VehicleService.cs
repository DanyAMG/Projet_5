using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Projet_5.Data;
using Projet_5.Models;

namespace Projet_5.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.Transactions)
                .Include(v => v.Advertisements)
                .Include(v => v.Repairs)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Transactions)
                .Include(v => v.Advertisements)
                .Include(v => v.Repairs)
                .ToListAsync();
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

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
