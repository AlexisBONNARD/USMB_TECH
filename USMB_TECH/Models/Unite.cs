using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("unite")]
    public partial class Unite
    {
        [Key]
        [Column("id_unite")]
        public int Id_Unite { get; set; }

        [Column("nom_unite")]
        [MaxLength(20)]
        public string Nom_Unite { get; set; }

        [InverseProperty(nameof(Consommable.UniteNavigation))]
        public virtual ICollection<Consommable> Unites { get; set; } = new List<Consommable>();

    }
}
