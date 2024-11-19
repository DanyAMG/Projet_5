using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projet_5.Models;

namespace Projet_5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //entities
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Repair> Repairs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //One-to-Many relation between Vehicle and Announcement
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Annoucements)
                .WithOne(a => a.Vehicle)
                .HasForeignKey(a => a.VehicleId);

            //One-to-Many relation between Vehicle and Transaction
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Transactions)
                .WithOne(t => t.Vehicle)
                .HasForeignKey(t => t.VehicleId);

            //One-to-Many relation between Vehicle and Repair
            modelBuilder.Entity<Vehicle>()
                .HasMany(v=> v.Repairs)
                .WithOne(r => r.Vehicle)
                .HasForeignKey(r => r.VehicleId);

            //One-to-Many relation between Announcement and Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Annoucement)
                .WithMany(a => a.Transaction)
                .HasForeignKey(t => t.AnnouncementId);

            //One-to-Many relation between Announcement and Repair
            modelBuilder.Entity<Repair>()
                .HasOne(r => r.Annoucement)
                .WithMany(a => a.Repair)
                .HasForeignKey(r => r.AnnouncementId);
        }
    } 
}
