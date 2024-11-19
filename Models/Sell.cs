using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Sell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Primary Key
        public float Amount { get; set; }

        [Column(TypeName = "DateTime2")] 
        public DateTime SellingDate { get; set; }


        //Foreigner Key
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        [NotMapped]
        public string VIN => Vehicle?.VIN;
    }
}
