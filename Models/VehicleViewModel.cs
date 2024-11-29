using System.ComponentModel.DataAnnotations;

namespace Projet_5.Models
{
    public class VehicleViewModel
    {
        [Required(ErrorMessage = "Le numéro VIN est obligatoire.")]
        [StringLength(17, ErrorMessage = "Le VIN doit contenir 17 caractères maximum.")]
        public string VIN { get; set; }

        [Required(ErrorMessage = "La marque est obligatoire.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Le modèle est obligatoire.")]
        public string Model { get; set; }

        [Range(1990, 9999, ErrorMessage = "Veuillez entrer une année valide.")]
        public int Year { get; set; }

        [StringLength(50, ErrorMessage = "La finition ne peut pas dépasser 50 caractères.")]
        public string Finition { get; set; }
    }
}