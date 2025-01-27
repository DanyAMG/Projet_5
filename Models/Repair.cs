using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{

    /// <summary>
    /// Repair est l'entité qui représente les réparations éffectués sur les véhicules, on y retrouve ses propriétés et ses relations.
    /// </summary>
    public class Repair
    {
        /// <summary>
        /// Identifiant unique de la réparation généré automatiquement par la base de données.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nom de la réparation.
        /// </summary>
        [Required]
        public string Reparation { get; set; }

        /// <summary>
        /// Cout de la réparation en euros.
        /// </summary>
        [Required]
        [Range(0.1, float.MaxValue)]
        public float Cost { get; set; }


        /// <summary>
        /// Identifiant unique du véhicule associé à la réparation.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Instance de l'objet Vehicle(Vehicule) associé à la réparation contenant les détails du véhicule.
        /// </summary>
        public Vehicle Vehicle { get; set; }

        /// <summary>
        /// Instance de l'objet Advertisement(Annonce) associé à la réparation contenant les détails du véhicule.
        /// </summary>
        public Advertisement Advertisements { get; set; }
    }
}
