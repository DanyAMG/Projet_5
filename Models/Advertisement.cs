using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key
        public DateTime DisponibilityDate { get; set; }
        public bool Disponibility { get; set; }
        public bool Selled { get; set; }
        public string Description { get; set; }
        public string? PhotoPath { get; set; }


        //Foreigner Key to Vehicle
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public ICollection<Transaction> Transaction { get; set; }

    }
}
