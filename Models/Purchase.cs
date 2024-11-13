using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Projet_5.Models;

namespace Projet_5.Models
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float BuyingPrice { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime PurchaseDate { get; set; }
        //Foreigner Key
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        [NotMapped]
        public string VIN=> Vehicle?.VIN;
    }
}
