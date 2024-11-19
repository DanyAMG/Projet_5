using Microsoft.EntityFrameworkCore;
using Projet_5.Data;

namespace Projet_5.Models.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsByVinAsync(string vin)
        {
            if(string.IsNullOrWhiteSpace(vin))
            {
                throw new ArgumentNullException("VIN cannot be null or empty.", nameof(vin));
            }
            else
            {
                var transactions = await _context.Set<Transaction>()
                    .Include(t => t.Vehicle)
                    .Where(t => t.Vehicle.VIN == vin)
                    .ToListAsync();

                return transactions;
            }
        }

        public async Task<List<Transaction>> GetAllTransactionAsync()
        {
            return await _context.Set<Transaction>()
                .Include(t => t.Vehicle)
                .ToListAsync();
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            _context.Set<Transaction>().Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction, string vin)
        {
            var existingTransactions = await GetTransactionsByVinAsync(vin);

            if (existingTransactions == null)
            {
                return false;
            }
            else
            {
                var existingTransaction = existingTransactions.FirstOrDefault(t => t.Id == transaction.Id);
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
        }
    }
}
