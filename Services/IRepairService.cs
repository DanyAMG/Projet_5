using Projet_5.Models;

namespace Projet_5.Services
{
    public interface IRepairService
    {
        Task<Repair> GetRepairByIdAsync(int id);
        Task<List<Repair>> GetRepairByAdvertisementAsync(int advertisementId);
        Task<Repair> AddRepairAsync(int vehicleId, Repair repair);
        Task UpdateRepairAsync(Repair repair);
        Task RemoveRepairAsync(int id);
        Task<List<Repair>> GetRepairsByVehicleIdAsync(int vehicleId);
    }
}