using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Qualifier
    {
        public int Id_Equipement { get; set; }

        public int Id_Mot_Clef { get; set; }

        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        public virtual Mot_Clef? Mot_ClefNavigation { get; set; } = null!;
    }
}
