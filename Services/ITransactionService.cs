using Projet_5.Models;

namespace Projet_5.Services
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransactionsByIdAsync(int id);
        Task<List<Transaction>> GetAllTransactionAsync();
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<bool> UpdateTransactionAsync(Transaction transaction, int id);
        Task<bool> DeleteTransactionAsync(int id);
    }
}
