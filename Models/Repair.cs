using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Repair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Primary Key
        public string Reparation { get; set; }
        public float Cost { get; set; }


        //Foreigner Key to Vehicle
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        //Foreigner Key to Advertisement
        public Advertisement Advertisements { get; set; }
    }
}
