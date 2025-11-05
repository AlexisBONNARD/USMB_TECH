using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("specifier")]
    public partial class Specifier
    {
        [Key]
        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Key]
        [Column("id_mot_clef")]
        public int Id_Mot_Clef { get; set; }

        [ForeignKey("id_plateforme")]
        [InverseProperty(nameof(Plateforme.Specifiers))]
        public virtual Plateforme? PlateformeNavigation { get; set; } = null!;

        [ForeignKey("id_mot_clef")]
        [InverseProperty(nameof(Mot_Clef.Specifiers))]
        public virtual Mot_Clef? Mot_ClefNavigation { get; set; } = null!;
    }
}
