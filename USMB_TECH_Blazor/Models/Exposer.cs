using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Exposer
    {
        public int Id_Plateforme { get; set; }

        public int Id_Thematique { get; set; }

        public virtual Plateforme? PlateformeNavigation { get; set; } = null!;

        public virtual Thematique? ThematiqueNavigation { get; set; } = null!;
    }
}
