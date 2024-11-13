namespace Projet_5.Models.Services
{
    public interface IRepairService
    {
        Task<List<Repair>> GetRepairByVinAsync(string vin);
        Task<Repair> AddRepairAsync(Repair repair);
        Task<Repair> UpdateRepairAsync(Repair repair);
        Task<bool> RemoveRepairAsync(int id);
    }
}