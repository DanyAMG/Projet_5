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

        
        //Foreigner Key to repair
        public ICollection<Repair> Repairs { get; set; } //one-to-many relation with Repair

        //Foreigner Key to Transaction
        public ICollection<Transaction> Transactions { get; set; } //one-to-many relation with Transaction

        //Foreigner Key to Announcement
        public ICollection<Announcement> Annoucements { get; set; } //one-to-many relation with Announcement
    }
}
