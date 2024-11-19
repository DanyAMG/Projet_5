namespace Projet_5.Models.Services
{
    public interface IAnnouncementService
    {
        Task<bool> SetDisponibilityAsync(bool disponibility, bool selled);
        void ArchiveAnnoucement(bool selled);
        Task<bool> UpdateDescriptionAsync(int announcementId, string description);
        Task<bool> UpdatePhotoAsync(int announcementId, string photo);
    }
}
