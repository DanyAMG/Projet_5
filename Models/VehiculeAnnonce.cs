namespace Projet_5.Models
{
    public class VehiculeAnnonce
    {
        public int Id { get; set; }

        //Foreigner Key to Vehicle
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }


        //Foreigner Key to Transaction
        public ICollection<Transaction> Transactions { get; set; }

        //Foreigner Key to Repair
        public ICollection<Repair> Repairs { get; set; }


    }
}
