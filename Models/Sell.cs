namespace Projet_5.Models
{
    public class Sell
    {
        public int Id { get; set; } //Primary Key
        float SellingPrice { get; set; }
        DateOnly DisponibilityDate { get; set; }
        DateOnly SellingDate { get; set; }
        Boolean Selled { get; set; }

        //Foreigner Key
        public string VIN { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
