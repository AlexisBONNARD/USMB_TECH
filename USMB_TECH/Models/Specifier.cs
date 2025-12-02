using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("specifier")]
    public partial class Specifier
    {
        [Column("id_pole_expertise")]
        public int Id_Pole_Expertise { get; set; }

        [Column("id_mot_clef")]
        public int Id_Mot_Clef { get; set; }

        [ForeignKey("Id_Pole_Expertise")]
        [InverseProperty(nameof(Pole_Expertise.Specifiers))]
        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

        [ForeignKey("Id_Mot_Clef")]
        [InverseProperty(nameof(Mot_Clef.Specifiers))]
        public virtual Mot_Clef? Mot_ClefNavigation { get; set; } = null!;
    }
}
