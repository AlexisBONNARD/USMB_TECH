using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("est_lier")]
    public partial class Est_Lier
    {
        [Column("nom_court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("id_thematique")]
        public int Id_Thematique { get; set; }

        [ForeignKey("nom_court")]
        [InverseProperty(nameof(Laboratoire.Est_Liers))]
        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        [ForeignKey("id_thematique")]
        [InverseProperty(nameof(Thematique.Est_Liers))]
        public virtual Thematique? ThematiqueNavigation { get; set; } = null!;
    }
}
