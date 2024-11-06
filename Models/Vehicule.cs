using System.ComponentModel.DataAnnotations;

namespace Projet_5.Models
{
    public class Vehicule
    {
        [Key] //Primary Key
        public string VIN { get; set; } //Vehicule Identification Number, unique number attributed to each vehicule
        
        public int Year { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Finition { get; set; }

        //A vehicule can just be purchased or selled one time
        public Purchase Purchase { get; set; } // one-to-one relation with Purchase
        public Sell Sell { get; set; } //one-to-one relation with Sell

        //A vehicule can have one or more repairs
        public List<Repair> Repairs { get; set; } //one-to-many relation with Repair
    }
}
