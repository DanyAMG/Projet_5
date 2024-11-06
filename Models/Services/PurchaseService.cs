using Microsoft.EntityFrameworkCore;
using Projet_5.Data;

namespace Projet_5.Models.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Purchase> GetPurchaseByVinAsync(string vin)
        {
            return await _context.Purchases
                .FirstOrDefaultAsync(p => p.VIN == vin);
        }

        public async Task<Purchase> AddPurchaseAsync(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return purchase;
        }

        public async Task<Purchase> UpdatePurchaseAsync(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
            await _context.SaveChangesAsync();
            return purchase;
        }
    }
}
