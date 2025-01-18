using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Primary Key
        public float Amount { get; set; }

        //type 0 = Buying transaction and 1 = Selling transaction
        public bool Type { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime TransactionDate { get; set; }


        //Foreigner Key to Vehicle
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        //Foreigner Key to Advertisement
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
