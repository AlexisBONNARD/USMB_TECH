using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("exposer")]
    public partial class Exposer
    {
        [Key]
        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("id_thematique")]
        public int Id_Thematique { get; set; }

        [ForeignKey("Id_Plateforme")]
        [InverseProperty(nameof(Plateforme.Exposers))]
        public virtual Plateforme? PlateformeNavigation { get; set; } = null!;

        [ForeignKey("Id_Thematique")]
        [InverseProperty(nameof(Thematique.Exposers))]
        public virtual Thematique? ThematiqueNavigation { get; set; } = null!;
    }
}
