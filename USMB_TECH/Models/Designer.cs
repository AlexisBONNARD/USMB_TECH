using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("designer")]
    public partial class Designer
    {
        [Column("id_mot_clef")]
        public int Id_Mot_Clef { get; set; }

        [Column("nom_court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [ForeignKey("id_mot_clef")]
        [InverseProperty(nameof(Mot_Clef.Designers))]
        public virtual Mot_Clef? Mot_ClefNavigation { get; set; } = null!;

        [ForeignKey("nom_court")]
        [InverseProperty(nameof(Laboratoire.Designers))]
        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;
    }
}
