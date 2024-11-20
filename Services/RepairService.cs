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

        public async Task<List<Repair>> GetRepairByVinAsync(string vin)
        {
            if (string.IsNullOrEmpty(vin))
            {
                throw new ArgumentException("VIN cannot be null or empty.", nameof(vin));
            }
            else
            {
                var repairs = await _context.Set<Repair>()
                                .Include(r => r.Vehicle)
                                .Where(r => r.Vehicle.VIN == vin)
                                .ToListAsync();

                return repairs;
            }
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

        public async Task<Repair> UpdateRepairAsync(Repair repair)
        {
            _context.Repairs.Update(repair);
            await _context.SaveChangesAsync();
            return repair;
        }

        public async Task<bool> RemoveRepairAsync(int id)
        {
            var repair = await _context.Repairs.FindAsync(id);
            if (repair != null)
            {
                _context.Repairs.Remove(repair);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
