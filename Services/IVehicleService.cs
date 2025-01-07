using Projet_5.Models;

namespace Projet_5.Services
{
    public interface IVehicleService
    {
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<List<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<bool> UpdateVehicleAsync(int id, Vehicle vehicle);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
