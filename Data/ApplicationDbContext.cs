using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projet_5.Models;

namespace Projet_5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //entities
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Repair> Repairs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
