using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Models
{
    public partial class Specifier
    {
        public int Id_Pole_Expertise { get; set; }

        public int Id_Mot_Clef { get; set; }

        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

        public virtual Mot_Clef? Mot_ClefNavigation { get; set; } = null!;
    }
}
