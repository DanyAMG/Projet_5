using Projet_5.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Projet_5.Models
{
    /// <summary>
    /// Modèle de données pour les véhicules dans l'application utilisé pour la validation et l'affichage dans les vues.
    /// </summary>
    public class VehicleViewModel
    {
        /// <summary>
        /// Identifiant unique du véhicule.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numero d'identification du véhicule (VIN) .
        /// </summary>
        [Required(ErrorMessage = "Le numéro VIN est obligatoire.")]
        [StringLength(17, ErrorMessage = "Le VIN doit contenir 17 caractères maximum.")]
        public string VIN { get; set; }

        /// <summary>
        /// Marque du véhicule
        /// </summary>
        [Required(ErrorMessage = "La marque est obligatoire.")]
        public string Brand { get; set; }

        /// <summary>
        /// Modele du véhicule
        /// </summary>
        [Required(ErrorMessage = "Le modèle est obligatoire.")]
        public string VehiculeModel { get; set; }

        /// <summary>
        /// Année de fabrication du véhicule.
        /// Peut être compris entre 1990 et l'année actuelle.
        /// </summary>
        [Required]
        [YearRange]
        public int Year { get; set; }

        /// <summary>
        /// Finition du véhicule.
        /// </summary>
        [StringLength(50, ErrorMessage = "La finition ne peut pas dépasser 50 caractères.")]
        public string Finition { get; set; }

        /// <summary>
        /// Photo du véhicule sous forme de fichié uploadé, optionnelle.
        /// </summary>
        public IFormFile? Photo { get; set; }

        /// <summary>
        /// Prix d'achat du véhicule.
        /// </summary>
        [Required(ErrorMessage = "Veuillez ajouter un prix d'achat.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix d'achat doit être supérieur à 0.")]
        public float Price { get; set; }

        /// <summary>
        /// Prix de vente du véhicule.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix d'achat doit être supérieur à 0.")]
        public float? SellingPrice { get; set; }
    }
}