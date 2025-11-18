using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Models
{
    public partial class Designer
    {
        public int Id_Mot_Clef { get; set; }

        public string Nom_Court { get; set; }

        public virtual Mot_Clef? Mot_ClefNavigation { get; set; } = null!;

        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;
    }
}