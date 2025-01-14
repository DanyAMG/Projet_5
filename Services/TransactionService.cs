using Humanizer;
using Microsoft.EntityFrameworkCore;
using Projet_5.Data;
using Projet_5.Models;

namespace Projet_5.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> GetTransactionsByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("VIN cannot be null or empty.", nameof(id));
            }
            else
            {
                var transaction = await _context.Transactions
                    .Include(t => t.Vehicle)
                    .FirstOrDefaultAsync(t => t.VehicleId == id);

                return transaction;
            }
        }

        public async Task<List<Transaction>> GetAllTransactionAsync()
        {
            return await _context.Set<Transaction>()
                .Include(t => t.Vehicle)
                .ToListAsync();
        }

        public async Task<Transaction> AddTransactionAsync(float amount, int vehicleId, int advertisementId)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.Now,
                VehicleId = vehicleId,
                AdvertisementId = advertisementId
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction, int id)
        {
            var existingTransaction = await GetTransactionsByIdAsync(id);

            if (existingTransaction == null)
            {
                return false;
            }
            else
            {
                existingTransaction.Id = transaction.Id;
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.TransactionDate = transaction.TransactionDate;

                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await GetTransactionsByIdAsync(id);
            if (transaction == null)
            {
                return false;
            }
            else
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();

                return true;
            }
        }

    }
}
