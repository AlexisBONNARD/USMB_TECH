using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Preciser
    {
        public int Id_Prestation { get; set; }
        public int Id_Mot_Clef { get; set; }
        public virtual Prestation? PrestationNavigation { get; set; } = null!;
        public virtual Mot_Clef? Mot_ClefNavigation { get; set; } = null!;
    }
}
