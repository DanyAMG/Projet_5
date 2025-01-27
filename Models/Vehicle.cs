using Projet_5.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{

    /// <summary>
    /// Vehicle est l'entité qui représente un véhicule dans l'application, on y retrouve ses propriétés et ses relations.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Identifiant unique du véhicule généré automatiquement par la base de données.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Numero VIN (Vehicle Identification Number) du véhicule.
        /// Doit contenir exclusivement 17 caractères.
        /// </summary>
        [Required]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Le numéro VIN doit contenir exactement 17 caractères.")]
        public string VIN { get; set; }

        /// <summary>
        /// Année de fabrication du véhicule, compris entre 1990 et l'année en cours.
        /// </summary>
        [Required]
        [YearRange]
        public int Year { get; set; }

        /// <summary>
        /// Marque du véhicule (exemple Toyota, Renault...)
        /// Ne doit pas dépasser 50 caractères.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "La marque ne doit pas dépasser 50 caractères.")]
        public string Brand { get; set; }

        /// <summary>
        /// Modèle du véhicule (exemple Prius, Clio...)
        /// Ne doit pas dépasser 50 caractères.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Le modèle ne doit pas dépasser 50 caractères.")]
        public string Model { get; set; }

        /// <summary>
        /// Finition du véhicule (RS, Luxe...)
        /// </summary>
        [StringLength(100, ErrorMessage = "La finition ne doit pas dépasser 100 caractères.")]
        public string Finition { get; set; }

        /// <summary>
        /// Chemin de la photo associé au véhicule.
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// Liste des annonces associées au véhicule.
        /// Relation 1-N avec la classe Advertisement.
        /// </summary>
        public ICollection<Advertisement> Advertisements { get; set; }

        /// <summary>
        /// Liste des transactions associées au véhicule.
        /// Relation 1-N avec la classe Transactions.
        /// </summary>
        public ICollection<Transaction> Transactions { get; set; }

        /// <summary>
        /// Liste des réparations associées au véhicule.
        /// Relaiton 1-N avec la classe Repair.
        /// </summary>
        public ICollection<Repair> Repairs { get; set; }
    }
}
