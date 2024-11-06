namespace Projet_5.Models
{
    public class Repair
    {
        public int Id { get; set; }
        public string RepaireType { get; set; }
        public float ReparationCost { get; set; }

        //Foreginer Key
        public string VIN { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
