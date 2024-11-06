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
                .FirstOrDefaultAsync(v => v.VIN == vin);
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Purchase)
                .Include(v => v.Sell)
                .Include(v => v.Repairs)
                .ToListAsync();
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task DeleteVehicleAsync(string vin)
        {
            var vehicle = await GetVehicleByVinAsync(vin);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
