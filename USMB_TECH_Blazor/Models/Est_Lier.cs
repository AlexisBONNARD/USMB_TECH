using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    [Table("est_lier")]
    public partial class Est_Lier
    {
        public string Nom_Court { get; set; }

        public int Id_Thematique { get; set; }

        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        public virtual Thematique? ThematiqueNavigation { get; set; } = null!;
    }
}
