using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Sell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Primary Key
        public float SellingPrice { get; set; }
        [Column(TypeName = "DateTime2")]
        DateTime DisponibilityDate { get; set; }
        [Column(TypeName = "DateTime2")] 
        public DateTime SellingDate { get; set; }
        Boolean Selled { get; set; }


        //Foreigner Key
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        [NotMapped]
        public string VIN => Vehicle?.VIN;
    }
}
