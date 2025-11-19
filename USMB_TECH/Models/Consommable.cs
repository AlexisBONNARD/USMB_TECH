using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("consommable")]
    public partial class Consommable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_consommable")]
        public int Id_Consommable {  get; set; }

        [Column("id_unite")]
        public int Id_Unite { get; set; }

        [Column("nom_consommable")]
        [MaxLength(50)]
        public string Nom_Consommable { get; set; }

        [Column("prix_unite")]
        [Precision(10, 2)]
        public double Prix_Unite { get; set; }

        [Column("prix_forfait")]
        [Precision(10, 2)]
        public double Prix_Forfait { get; set; }

        [Column("forfait")]
        public bool Forfait { get; set; }

        [ForeignKey("Id_Unite")]
        [InverseProperty(nameof(Unite.Consommables))]
        public virtual Unite? UniteNavigation { get; set; } = null!;

        [InverseProperty(nameof(Consommer.ConsommableNavigation))]
        public virtual ICollection<Consommer> Consommers { get; set; } = new List<Consommer>();
    }
}
