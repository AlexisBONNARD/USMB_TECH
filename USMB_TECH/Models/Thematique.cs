using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("thematique")]
    public class Thematique
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_thematique")]
        public int Id_Thematique { get; set; }

        [Column("id_sous_thematique")]
        public int Id_Sous_Thematique { get; set; }

        [Column("nom_thematique")]
        [MaxLength(50)]
        public string Nom_Thematique { get; set; }

        [ForeignKey("Id_Sous_Thematique")]
        [InverseProperty(nameof(Thematique.Thematiques))]
        public virtual Thematique? ThematiqueNavigation { get; set; } = null!;

        [InverseProperty(nameof(Thematique.ThematiqueNavigation))]
        public virtual ICollection<Thematique> Thematiques { get; set; } = new List<Thematique>();

        [InverseProperty(nameof(Est_Lier.ThematiqueNavigation))]
        public virtual ICollection<Est_Lier> Est_Liers { get; set; } = new List<Est_Lier>();

        [InverseProperty(nameof(Exposer.ThematiqueNavigation))]
        public virtual ICollection<Exposer> Exposers { get; set; } = new List<Exposer>();
    }
}
