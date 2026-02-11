using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Prise_Contact
    {
        public int Num_Prise_Contact { get; set; }

        public string? Nom_Court { get; set; }

        public int? Id_Equipement { get; set; }

        public int? Id_Pole_Expertise { get; set; }

        public int Id_Type_Client { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre nom")]
        [MaxLength(50, ErrorMessage = "Le nom Contact ne doit pas dépasser 50 caractères")]
        public string Nom_Contact { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre prénom")]
        [MaxLength(50, ErrorMessage = "Le prénom Contact ne doit pas dépasser 50 caractères")]
        public string Prenom_Contact { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre type d’entreprise")]
        [MaxLength(100, ErrorMessage = "Le nom de l’entreprise Contact ne doit pas dépasser 100 caractères")]
        public string Entreprise_Contact { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre email")]
        [EmailAddress(ErrorMessage = "Le format de l'adresse e-mail est invalide.")]
        [MaxLength(60, ErrorMessage = "L'email Contact ne doit pas dépasser 60 caractères")]
        public string Email_Contact { get; set; }

        [Required(ErrorMessage = "Veuillez décrire votre besoin")]
        [MaxLength(200, ErrorMessage = "La description du besoins ne doit pas dépasser 200 caractères")]
        public string Description_besoins { get; set; }

        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

        //public virtual Type_Client? Type_ClientNavigation { get; set; } = null!;
    }
}
