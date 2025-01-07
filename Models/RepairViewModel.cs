using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Projet_5.Models
{
    public class RepairViewModel
    {
        [Required(ErrorMessage = "Veuillez entrer une description de la réparation.")]
        public string Reparation { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le coût de la réparation.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le coût doit être supérieur à 0.")]
        public float Cost { get; set; }

        [Required]
        public int VehicleId { get; set; }
    }
}
