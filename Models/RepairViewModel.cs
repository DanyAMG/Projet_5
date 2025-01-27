using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Projet_5.Models
{
    /// <summary>
    /// Modèle de données pour les réparations utilisé dans l'application pour la validation des données.
    /// </summary>
    public class RepairViewModel
    {
        /// <summary>
        /// Nom de la réparation.
        /// </summary>
        [Required(ErrorMessage = "Veuillez entrer une description de la réparation.")]
        public string Reparation { get; set; }

        /// <summary>
        /// Cout de la réparation.
        /// </summary>
        [Required(ErrorMessage = "Veuillez entrer le coût de la réparation.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le coût doit être supérieur à 0.")]
        public float Cost { get; set; }

        /// <summary>
        /// Identifiant unique du véhicule associé à la réparation.
        /// </summary>
        [Required]
        public int VehicleId { get; set; }
    }
}
