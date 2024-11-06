namespace Projet_5.Models.Services
{
    public interface ISellService
    {
        Task<Sell> GetSellByVinAsync(string vin);
        Task<Sell> AddSellAsync(Sell sell);
        Task<Sell> UpdateSellAsync(Sell sell);
    }
}
