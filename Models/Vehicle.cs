using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string VIN { get; set; } //vehicle Identification Number, unique number attributed to each vehicle
        public int Year { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Finition { get; set; }
        public bool IsAvailable { get; set; }

        //A vehicle can just be purchased or selled one time
        public Purchase Purchase { get; set; } // one-to-one relation with Purchase
        public Sell Sell { get; set; } //one-to-one relation with Sell

        //A vehicle can have one or more repairs
        public List<Repair> Repairs { get; set; } //one-to-many relation with Repair
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Sell> Sells { get; set; }
    }
}
