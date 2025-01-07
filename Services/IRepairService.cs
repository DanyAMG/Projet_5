using Projet_5.Models;

namespace Projet_5.Services
{
    public interface IRepairService
    {
        Task<Repair> GetRepairByIdAsync(int id);
        Task<List<Repair>> GetRepairByAnnouncementAsync(int announcementId);
        Task<Repair> AddRepairAsync(Repair repair);
        Task UpdateRepairAsync(Repair repair);
        Task RemoveRepairAsync(int id);
        Task<List<Repair>> GetRepairsByVehicleIdAsync(int vehicleId);
    }
}