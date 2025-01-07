using Microsoft.EntityFrameworkCore;
using Projet_5.Data;
using Projet_5.Models;

namespace Projet_5.Services
{
    public class RepairService : IRepairService
    {
        private readonly ApplicationDbContext _context;

        public RepairService(ApplicationDbContext context)
        {
            _context = context;
        }

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

        public async Task<List<Repair>> GetRepairByAnnouncementAsync(int announcementId)
        {
            if (announcementId <= 0)
            {
                throw new ArgumentException("Announcement ID must be greater than 0", nameof(announcementId));
            }
            return await _context.Set<Repair>()
                .Include(r => r.Vehicle)
                .Where(r => r.Annoucement.Id == announcementId)
                .ToListAsync();
        }

        public async Task<Repair> AddRepairAsync(Repair repair)
        {
            _context.Repairs.Add(repair);
            await _context.SaveChangesAsync();
            return repair;
        }

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

        public async Task RemoveRepairAsync(int id)
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

        public async Task<List<Repair>> GetRepairsByVehicleIdAsync(int vehicleId)
        {
            return await _context.Repairs
                                 .Where(r => r.VehicleId == vehicleId)
                                 .ToListAsync();
        }
    }
}
