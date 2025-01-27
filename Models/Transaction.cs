using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_5.Models
{
    /// <summary>
    /// Transaction est l'entité qui représente chaque transaction d'achat ou de vente des véhicules, on y retrouve ses propriétés et ses relations.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Identifiant unique de la transaction généré automatiquement par la base de données.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Primary Key

        /// <summary>
        /// Valeur de la transaction en euros.
        /// </summary>
        [Required]
        [Range(0.1, float.MaxValue)]
        public float Amount { get; set; }

        /// <summary>
        /// Type de la transaction pour déterminer si c'est une vente ou un achat.
        /// False étant une transaction d'achat.
        /// True étant une transaction de vente.
        /// </summary>
        [Required]
        public bool Type { get; set; }

        /// <summary>
        /// Date de la transaction
        /// </summary>
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime TransactionDate { get; set; }


        /// <summary>
        /// Identifiant unique du véhicule lié à la transaction.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Instance de l'objet Vehicle(Véhicule) associé à la transaction contenant les détails du véhicule concerné.
        /// </summary>
        public Vehicle Vehicle { get; set; }

        /// <summary>
        /// Identifiant unique de l'annonce associé à cette transaction.
        /// </summary>
        public int AdvertisementId { get; set; }

        /// <summary>
        /// Instance de l'objet Advertisement(Annonce) associé à la transaction contenant les détails de l'annonce concernées.
        /// </summary>
        public Advertisement Advertisement { get; set; }
    }
}
