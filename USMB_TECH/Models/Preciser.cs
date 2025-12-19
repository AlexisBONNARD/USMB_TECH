using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("preciser")]
    public class Preciser
    {
        [Column("id_prestation")]
        public int Id_Prestation { get; set; }

        [Column("id_mot_clef")]
        public int Id_Mot_Clef { get; set; }

        [ForeignKey("Id_Prestation")]
        [InverseProperty(nameof(Prestation.Precisers))]
        public virtual Prestation? PrestationNavigation { get; set; } = null!;

        [ForeignKey("Id_Mot_Clef")]
        [InverseProperty(nameof(Mot_Clef.Precisers))]
        public virtual Mot_Clef? Mot_ClefNavigation { get; set; } = null!;
    }
}
