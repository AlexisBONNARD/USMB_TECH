using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Prise_Contact
    {
        public int Num_Prise_Contact { get; set; }

        public int? Id_Equipement { get; set; }

        public int? Id_Plateforme { get; set; }

        public int Id_Type_Client { get; set; }

        public string Nom_Contact { get; set; }

        public string Prenom_Contact { get; set; }

        public string Entreprise_Contact { get; set; }

        public string Email_Contact { get; set; }

        public string Description_besoins { get; set; }

        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        public virtual Plateforme? PlateformeNavigation { get; set; } = null!;

        //public virtual Type_Client? Type_ClientNavigation { get; set; } = null!;
    }
}
