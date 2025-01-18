using Projet_5.Models;

namespace Projet_5.Services
{
    public interface IAdvertisementService
    {
        Task<List<Advertisement>> GetAllAdvertisementsAsync();
        Task<Advertisement> GetAdvertisementByIdAsync(int id);
        Task<Advertisement> GetAdvertisementByVehicleIdAsync(int vehicleId);
        Task<Advertisement> AddAdvertisementAsync(Advertisement advertisement, Vehicle vehicle);
        Task<bool> UpdateAdvertisementAsync(Advertisement annoucement);
        Task<bool> DeleteAdvertisementAsync(int id);
        Task SetDisponibilityAsync(int vehicleId, bool disponibility);
        Task<float> CalculateSellingPriceAsync(int vehicleId);
        Task SetSelledAsync(int vehicleId, bool selled);
        void ArchiveAnnoucement(bool selled);
        Task<bool> UpdateDescriptionAsync(int advertisementId, string description);
        Task<bool> UpdatePhotoAsync(int advertisementId, string photo);
    }
}
