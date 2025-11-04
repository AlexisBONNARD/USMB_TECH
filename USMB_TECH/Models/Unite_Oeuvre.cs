using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("unite_oeuvre")]
    public partial class Unite_Oeuvre
    {
        [Key]
        [Column("id_unite_oeuvre")]
        public int Id_Unite_Oeuvre { get; set; }

        [Column("nom_unite_oeuvre")]
        [MaxLength(20)]
        public string Nom_Unite_Oeuvre { get; set; }
    }
}
