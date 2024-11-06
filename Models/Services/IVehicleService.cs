namespace Projet_5.Models.Services
{
    public interface IVehicleService
    {
        Task<Vehicle> GetVehicleByVinAsync(string VIN);
        Task<List<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(string VIN);
    }
}
