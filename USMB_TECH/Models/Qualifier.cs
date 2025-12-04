using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("qualifier")]
    public class Qualifier
    {
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_mot_clef")]
        public int Id_Mot_Clef { get; set; }

        [ForeignKey("Id_Equipement")]
        [InverseProperty(nameof(Equipement.Qualifiers))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        [ForeignKey("Id_Mot_Clef")]
        [InverseProperty(nameof(Mot_Clef.Qualifiers))]
        public virtual Mot_Clef? Mot_ClefNavigation { get; set; } = null!;
    }
}
