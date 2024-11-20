using Projet_5.Models;

namespace Projet_5.Services
{
    public interface IAnnouncementService
    {
        Task<List<Announcement>> GetAllAnnouncementsAsync();
        Task<Announcement> GetAnnouncementByIdAsync(int id);
        Task<Announcement> AddAnnouncementAsync(Announcement announcement);
        Task<bool> UpdateAnnouncementAsync(Announcement annoucement);
        Task<bool> DeleteAnnouncementAsync(int id);
        Task<bool> SetDisponibilityAsync(bool disponibility, bool selled);
        void ArchiveAnnoucement(bool selled);
        Task<bool> UpdateDescriptionAsync(int announcementId, string description);
        Task<bool> UpdatePhotoAsync(int announcementId, string photo);
    }
}
