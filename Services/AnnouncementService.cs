using Microsoft.EntityFrameworkCore;
using Projet_5.Data;
using Projet_5.Models;
using System;

namespace Projet_5.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Announcement>> GetAllAnnouncementsAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(int id)
        {
            return await _context.Announcements.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Announcement> AddAnnouncementAsync(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<bool> UpdateAnnouncementAsync(Announcement announcement)
        {
            var existingAnnouncement = await GetAnnouncementByIdAsync(announcement.Id);
            if (existingAnnouncement == null)
            {
                return false;
            }

            existingAnnouncement.Description = announcement.Description;
            existingAnnouncement.PhotoPath = announcement.PhotoPath;
            existingAnnouncement.Disponibility = announcement.Disponibility;
            existingAnnouncement.DisponibilityDate = announcement.DisponibilityDate;
            existingAnnouncement.selled = announcement.selled;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAnnouncementAsync(int id)
        {
            var announcement = await GetAnnouncementByIdAsync(id);
            if (announcement == null)
            {
                return false;
            }

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SetDisponibilityAsync(bool disponibility, bool selled)
        {
            var announcement = await _context.Set<Announcement>().FirstOrDefaultAsync(a => a.Disponibility == !disponibility);

            if (announcement == null)
            {
                return false;
            }

            announcement.Disponibility = disponibility;
            announcement.selled = selled;


            await _context.SaveChangesAsync();
            return true;
        }

        public void ArchiveAnnoucement(bool selled)
        {
            var announcementsToArchive = _context.Set<Announcement>().Where(a => !a.selled).ToList();

            foreach (var announcement in announcementsToArchive)
            {
                announcement.selled = selled;
            }
            _context.SaveChanges();
        }

        public async Task<bool> UpdateDescriptionAsync(int announcementId, string description)
        {
            var announcement = await _context.Set<Announcement>().FirstOrDefaultAsync(a => a.Id == announcementId);

            if (announcement == null)
            {
                return false;
            }

            announcement.Description = description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePhotoAsync(int announcementId, string photo)
        {
            var announcement = await _context.Set<Announcement>().FirstOrDefaultAsync(a => a.Id == announcementId);

            if (announcement == null)
            {
                return false;
            }

            announcement.PhotoPath = photo;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
