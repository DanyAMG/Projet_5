namespace Projet_5.Models.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsByVinAsync(string vin);
        Task<List<Transaction>> GetAllTransactionAsync();
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<bool> UpdateTransactionAsync(Transaction transaction, string vin);
    }
}
