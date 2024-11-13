namespace Projet_5.Models.Services
{
    public interface IVehicleService
    {
        Task<Vehicle> GetVehicleByVinAsync(string VIN);
        Task<List<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<Vehicle> UpdateVehicleAsync(string vin, Vehicle vehicle);
        Task<bool> DeleteVehicleAsync(string VIN);

        Task<bool> MarkVehicleAsAvailable(string vin);
        Task<bool> MarkVehicleAsUnavailable(string vin);
    }
}
