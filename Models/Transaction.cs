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

        [Column(TypeName = "DateTime2")]
        public DateTime TransactionDate { get; set; }


        //Foreigner Key to Vehicle
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        //Foreigner Key to Announcement
        public int? AnnouncementId { get; set; }
        public Announcement Annoucement { get; set; }
    }
}
