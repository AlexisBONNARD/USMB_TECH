using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("Fonction")]
    public partial class Fonction
    {
        [Key]
        [Column("Id_Fonction")]
        public int Id_Fonction { get; set; }

        [Column("Nom_Fonction")]
        [StringLength(50)]
        public string Nom_Fonction { get; set; }
    }
}
