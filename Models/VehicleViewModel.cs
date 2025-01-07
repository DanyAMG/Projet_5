using System.ComponentModel.DataAnnotations;

namespace Projet_5.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le numéro VIN est obligatoire.")]
        [StringLength(17, ErrorMessage = "Le VIN doit contenir 17 caractères maximum.")]
        public string VIN { get; set; }

        [Required(ErrorMessage = "La marque est obligatoire.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Le modèle est obligatoire.")]
        public string VehiculeModel { get; set; }

        [Range(1990, 9999, ErrorMessage = "Veuillez entrer une année valide.")]
        public int Year { get; set; }

        [StringLength(50, ErrorMessage = "La finition ne peut pas dépasser 50 caractères.")]
        public string Finition { get; set; }

        [Required(ErrorMessage = "Veuillez ajouter une photo.")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Veuillez ajouter un prix d'achat.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix d'achat doit être supérieur à 0.")]
        public float Price { get; set; }
    }
}