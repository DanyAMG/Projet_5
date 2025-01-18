using Projet_5.Models;

namespace Projet_5.Services
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<Transaction> GetBuyingTransactionByVehicleIdAsync(int id);
        Task<Transaction> GetSellingTransactionByVehicleIdAsync(int id);
        Task<List<Transaction>> GetAllTransactionAsync();
        Task<Transaction> AddTransactionAsync(float amount, int vehicleId, int advertisementId, bool type);
        Task<bool> UpdateTransactionAsync(Transaction transaction, int id);
        Task<bool> DeleteTransactionAsync(int id);
    }
}
