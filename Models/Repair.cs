using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Repair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Primary Key
        public string RepaireType { get; set; }
        public float ReparationCost { get; set; }

        //Foreginer Key
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        [NotMapped]
        public string VIN => Vehicle?.VIN;
    }
}
