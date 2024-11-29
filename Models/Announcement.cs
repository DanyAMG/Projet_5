using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key
        public DateTime DisponibilityDate { get; set; }
        public bool Disponibility { get; set; }
        public bool selled { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }


        [ForeignKey(nameof(VehiculeAnnonce))]
        public int VehiculeAnnonceId { get; set; }
        public VehiculeAnnonce VehiculeAnnonce { get; set; }
    }
}
