using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Posseder
    {
        public int Id_Equipement { get; set; }

        public int Id_Fonctionalite { get; set; }

        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        public virtual Fonctionalite? FonctionaliteNavigation { get; set; } = null!;
    }
}
