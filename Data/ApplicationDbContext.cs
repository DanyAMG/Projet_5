using Microsoft.AspNetCore.Identity;
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
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Repair> Repairs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "User", NormalizedName = "USER" });

            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.Vehicle)
                .WithMany(v => v.Advertisements)
                .HasForeignKey(a => a.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
