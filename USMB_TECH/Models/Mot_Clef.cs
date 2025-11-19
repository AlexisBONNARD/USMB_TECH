using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("mot_clef")]
    public partial class Mot_Clef
    {
        [Column("id_mot_clef")]
        public int Id_Mot_Clef { get; set; }

        [Column("nom_mot_clef")]
        [MaxLength(25)]
        public string Nom_Mot_Clef { get; set; }

        [InverseProperty(nameof(Designer.Mot_ClefNavigation))]
        public virtual ICollection<Designer> Designers { get; set; } = new List<Designer>();

        [InverseProperty(nameof(Specifier.Mot_ClefNavigation))]
        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();
    }
}
