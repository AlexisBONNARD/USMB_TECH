using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Proposer
    {
        public int Id_Equipement { get; set; }

        public int Id_Type_Utilisation { get; set; }

        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        public virtual Type_Utilisation? Type_UtilisationNavigation { get; set; } = null!;
    }
}
