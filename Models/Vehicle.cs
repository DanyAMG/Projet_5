using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Le numéro VIN doit contenir exactement 17 caractères.")]
        public string VIN { get; set; }

        [Range(1990, 2100, ErrorMessage = "L'année doit être comprise entre 1990 et 2100.")]
        public int Year { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La marque ne doit pas dépasser 50 caractères.")]
        public string Brand { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Le modèle ne doit pas dépasser 50 caractères.")]
        public string Model { get; set; }

        [StringLength(100, ErrorMessage = "La finition ne doit pas dépasser 100 caractères.")]
        public string Finition { get; set; }


        public ICollection<Announcement> Announcements { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Repair> Repairs { get; set; }
    }
}
