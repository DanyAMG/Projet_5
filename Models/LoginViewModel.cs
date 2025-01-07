using System.ComponentModel.DataAnnotations;

namespace Projet_5.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nom d'utilisateur")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }
    }
}
