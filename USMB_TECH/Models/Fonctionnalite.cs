using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("fonctionnalite")]
    public partial class Fonctionnalite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_fonctionnalite")]
        public int Id_Fonctionnalite { get; set; }

        [Column("nom_fonctionnalite")]
        [MaxLength(50)]
        public string Nom_Fonctionnalite { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [InverseProperty(nameof(Posseder.FonctionnaliteNavigation))]
        public virtual ICollection<Posseder> Posseders { get; set; } = new List<Posseder>();
    }
}
