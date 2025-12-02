using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Exposer
    {
        public int Id_Pole_Expertise { get; set; }

        public int Id_Thematique { get; set; }

        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

        public virtual Thematique? ThematiqueNavigation { get; set; } = null!;
    }
}
