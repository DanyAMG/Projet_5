using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        float BuyingPrice { get; set; }
        DateOnly PurchaseDate { get; set; }

        //Foreigner Key
        [ForeignKey(nameof(vehicle))]
        public string VIN {  get; set; }
        public Vehicle vehicle { get; set; }
    }
}
