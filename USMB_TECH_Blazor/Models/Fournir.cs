using USMB_TECH_Blazor.Models;

namespace USMB_TECH.Models
{
    public partial class Fournir
    {
        public int Id_Equipement { get; set; }

        public int Id_Prestation { get; set; }

        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        public virtual Prestation? PrestationNavigation { get; set; } = null!;
    }
}
