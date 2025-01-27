using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    /// <summary>
    /// Advertisement est l'entité qui représente les annonces des véhiculesn, on y retrouve ses propriétés et ses relations.
    /// </summary>
    public class Advertisement
    {
        /// <summary>
        /// Identifiant unique de l'annonce généré automatiquement par la base de données.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Date de la mise en vente du véhicule.
        /// </summary>
        public DateTime DisponibilityDate { get; set; }

        /// <summary>
        /// Indique si le véhicule est disponible à la vente.
        /// True étant un véhicule disponible.
        /// False étant un véhicule non disponible.
        /// </summary>
        public bool Disponibility { get; set; }

        /// <summary>
        /// Indique si le véhicule a été vendu.
        /// True étant un véhicule vendu.
        /// False étant un véhicule invendu.
        /// </summary>
        public bool Selled { get; set; }

        /// <summary>
        /// Description du véhicule visible dans l'annonce.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Chemin de la photo du véhicule.
        /// </summary>
        public string? PhotoPath { get; set; }


        /// <summary>
        /// Identifiant unique du véhicule associé à l'annonce.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Instance de l'objet Vehicle(Vehicule) associé à l'annonce contenant les détails du vehicule.
        /// </summary>
        public Vehicle Vehicle { get; set; }

        /// <summary>
        /// Liste des objets Transaction associée à l'annonce contenant les détails des transactions.
        /// </summary>
        public ICollection<Transaction> Transaction { get; set; }

    }
}
