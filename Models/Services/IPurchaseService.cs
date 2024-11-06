namespace Projet_5.Models.Services
{
    public interface IPurchaseService
    {
        Task<Purchase> GetPurchaseByVinAsync(string vin);
        Task<Purchase> AddPurchaseAsync(Purchase purchase);
        Task<Purchase> UpdatePurchaseAsync(Purchase purchase);
    }
}
 