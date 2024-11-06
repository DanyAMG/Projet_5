namespace Projet_5.Models.Services
{
    public interface IVehiculeService
    {
        Task<Vehicule> GetVehiculeAsync(string VIN);
        Task<List<Vehicule>> GetAllVehiculesAsync();
        Task<Vehicule> AddVehiculeAsync(Vehicule vehicule);
        Task<Vehicule> UpdateVehiculeAsync(Vehicule vehicule);
        Task DeleteVehiculeAsync(string VIN);
    }
}
