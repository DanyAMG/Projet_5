using Microsoft.EntityFrameworkCore;
using Projet_5.Data;

namespace Projet_5.Models.Services
{
    public class SellService : ISellService
    {
        private readonly ApplicationDbContext _context;

        public SellService(ApplicationDbContext context)
        {

        _context = context;
        }

        public async Task<Sell> GetSellByVinAsync(string vin)
        {
            return await _context.Sells
                .FirstOrDefaultAsync(s => s.VIN == vin);
        }

        public async Task<Sell> AddSellAsync(Sell sell)
        {
            _context.Sells.Add(sell);
            await _context.SaveChangesAsync();
            return sell;
        }

        public async Task<Sell> UpdateSellAsync(Sell sell)
        {
            _context.Sells.Update(sell);
            await _context.SaveChangesAsync();
            return sell;
        }
    }
}
