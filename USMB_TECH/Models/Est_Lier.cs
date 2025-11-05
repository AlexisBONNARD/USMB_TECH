using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("est_Lier")]
    public partial class Est_Lier
    {
        [Column("Nom_Court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("Id_Thematique")]
        public int Id_Thematique { get; set; }

        [ForeignKey("Nom_Court")]
        [InverseProperty(nameof(Laboratoire.Est_Liers))]
        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        [ForeignKey("Id_Thematique")]
        [InverseProperty(nameof(Thematique.Est_Liers))]
        public virtual Thematique? ThematiqueNavigation { get; set; } = null!;
    }
}
