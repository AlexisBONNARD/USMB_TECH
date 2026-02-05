using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Autoriser
    {
        public int Id_Equipement { get; set; }

        public int Id_Type_Client { get; set; }

        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        public virtual Type_Client? Type_ClientNavigation { get; set; } = null!;
    }
}
