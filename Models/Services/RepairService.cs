using Microsoft.EntityFrameworkCore;
using Projet_5.Data;

namespace Projet_5.Models.Services
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
            return await _context.Repairs
                .Where(r => r.VIN == vin)
                .ToListAsync();
        }

        public async Task<Repair>AddRepairAsync(Repair repair)
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

        public async Task RemoveRepairAsync(int id)
        {
            var repair = await _context.Repairs.FindAsync(id);
            if (repair != null)
            {
                _context.Repairs.Remove(repair);
                await _context.SaveChangesAsync();
            }
        }
    }
}
