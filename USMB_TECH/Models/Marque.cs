using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("marque")]   
    public partial class Marque
    {
        [Key]
        [Column("id_marque")]
        public int Id_Marque { get; set; }

        [Column("nom_marque")]
        [MaxLength(50)]
        public string Nom_Marque { get; set; }

        [InverseProperty(nameof(Modele.MarqueNavigation))]
        public virtual ICollection<Modele> Modeles { get; set; } = new List<Modele>();
    }
}
