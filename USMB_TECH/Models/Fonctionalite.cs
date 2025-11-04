using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("fonctionalite")]
    public partial class Fonctionalite
    {
        [Key]
        [Column("id_fonctionalite")]
        public int Id_Fonctionalite { get; set; }

        [Column("nom_fonctionalite")]
        [MaxLength(50)]
        public string Nom_Fonctionalite { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
