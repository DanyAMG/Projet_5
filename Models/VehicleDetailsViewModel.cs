using Projet_5.Models;

/// <summary>
/// Modèle de données pour afficher les détails des véhicules dans l'application.
/// </summary>
public class VehicleDetailsViewModel
{
    /// <summary>
    /// Instance de l'objet Vehicle(Vehicule) comtenant les détails du véhicule.
    /// </summary>
    public Vehicle Vehicle { get; set; }

    /// <summary>
    /// Liste des réparations.
    /// </summary>
    public List<Repair> Repairs { get; set; } = new List<Repair>();

    /// <summary>
    /// Instance de Transaction comprenant les détails de la transaction d'achat du véhicule.
    /// </summary>
    public Transaction BuyingTransaction { get; set; }

    /// <summary>
    /// Prix de vente du véhicule.
    /// </summary>
    public float SellingPrice { get; set; }

    /// <summary>
    /// Instance de l'objet Advertisement(Annonce) comprenant les détails de l'annonce.
    /// </summary>
    public Advertisement Advertisement { get; set; }

}