using Projet_5.Models;

public class VehicleDetailsViewModel
{
    public Vehicle Vehicle { get; set; }
    public List<Repair> Repairs { get; set; } = new List<Repair>();
    public Transaction Transaction { get; set; }
    public Advertisement Advertisement { get; set; }

}