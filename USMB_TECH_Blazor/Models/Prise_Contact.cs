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
        public string Nom_Contact { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre prénom")]
        public string Prenom_Contact { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre type d’entreprise")]
        public string Entreprise_Contact { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre email")]
        [EmailAddress(ErrorMessage = "Le format de l'adresse e-mail est invalide.")]
        public string Email_Contact { get; set; }

        [Required(ErrorMessage = "Veuillez décrire votre besoin")]
        public string Description_besoins { get; set; }

        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

        //public virtual Type_Client? Type_ClientNavigation { get; set; } = null!;
    }
}
