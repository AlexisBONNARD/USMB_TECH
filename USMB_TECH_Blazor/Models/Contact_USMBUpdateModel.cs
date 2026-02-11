using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH.Models;

namespace USMB_TECH_Blazor.Models
{
    public class Contact_USMBUpdateModel
    {
        public int Id_Contact { get; set; }

        public int Id_Fonction { get; set; }

        [Required(ErrorMessage = "Le nom du contact est obligatoire.")]
        [MaxLength(50, ErrorMessage = "Le nom Contact ne doit pas dépasser 50 caractères.")]
        public string Nom_Contact { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom du contact est obligatoire.")]
        [MaxLength(50, ErrorMessage = "Le prénom Contact ne doit pas dépasser 50 caractères.")]
        public string Prenom_Contact { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le code RH est obligatoire.")]
        [MaxLength(25, ErrorMessage = "Le code RH ne doit pas dépasser 25 caractères.")]
        public string Code_RH { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'adresse e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Le format de l'adresse e-mail est invalide.")]
        [MaxLength(50, ErrorMessage = "L'adresse e-mail ne doit pas dépasser 50 caractères.")]
        public string Mail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire.")]
        [Phone(ErrorMessage = "Le format du numéro de téléphone est invalide.")]
        [MaxLength(50, ErrorMessage = "Le numéro de téléphone ne doit pas dépasser 50 caractères.")]
        public string Telephone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le laboratoire du contact est obligatoire.")]
        [ForeignKey("LaboratoireNavigation")]
        public string Nom_Court { get; set; } = string.Empty;

        public string Nom_Fonction { get; set; } = string.Empty;

        public virtual Fonction? FonctionNavigation { get; set; } = null!;

        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();
    }
}

