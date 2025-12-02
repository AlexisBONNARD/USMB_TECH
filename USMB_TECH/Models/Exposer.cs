using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("exposer")]
    public partial class Exposer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_pole_expertise")]
        public int Id_Pole_Expertise { get; set; }

        [Column("id_thematique")]
        public int Id_Thematique { get; set; }

        [ForeignKey("Id_Pole_Expertise")]
        [InverseProperty(nameof(Pole_Expertise.Exposers))]
        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

        [ForeignKey("Id_Thematique")]
        [InverseProperty(nameof(Thematique.Exposers))]
        public virtual Thematique? ThematiqueNavigation { get; set; } = null!;
    }
}
