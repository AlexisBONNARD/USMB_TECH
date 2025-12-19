using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("exposer")]
    public partial class Exposer
    {
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_thematique")]
        public int Id_Thematique { get; set; }

        [ForeignKey("Id_Pole_Expertise")]
        [InverseProperty(nameof(Equipement.Exposers))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        [ForeignKey("Id_Thematique")]
        [InverseProperty(nameof(Thematique.Exposers))]
        public virtual Thematique? ThematiqueNavigation { get; set; } = null!;
    }
}
