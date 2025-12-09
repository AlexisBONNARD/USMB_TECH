using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Contact_USMB
    {
        [Key]
        public int Id_Contact { get; set; }

        public int Id_Fonction { get; set; }

        [Required(ErrorMessage = "Le nom du contact est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        public string Nom_Contact { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom du contact est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le prénom ne peut pas dépasser 100 caractères.")]
        public string Prenom_Contact { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le code RH est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le code RH ne peut pas dépasser 50 caractères.")]
        public string Code_RH { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro de sécurité sociale est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le numéro de sécurité sociale ne peut pas dépasser 50 caractères.")]
        public string Num_Securite_Social { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'adresse e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Le format de l'adresse e-mail est invalide.")]
        [StringLength(100, ErrorMessage = "L'adresse e-mail ne peut pas dépasser 100 caractères.")]
        public string Mail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire.")]
        [Phone(ErrorMessage = "Le format du numéro de téléphone est invalide.")]
        [StringLength(20, ErrorMessage = "Le numéro de téléphone ne peut pas dépasser 20 caractères.")]
        public string Telephone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le laboratoire du contact est obligatoire.")]
        [ForeignKey("LaboratoireNavigation")]
        public string Nom_Court { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fonction du contact est obligatoire.")]
        public string Nom_Fonction { get; set; } = string.Empty;

        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();
    }
}
