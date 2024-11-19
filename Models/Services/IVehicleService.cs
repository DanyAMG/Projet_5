namespace Projet_5.Models.Services
{
    public interface IVehicleService
    {
        Task<Vehicle> GetVehicleByVinAsync(string vin);
        Task<List<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<bool> UpdateVehicleAsync(string vin, Vehicle vehicle);
        Task<bool> DeleteVehicleAsync(string vin);
    }
}
