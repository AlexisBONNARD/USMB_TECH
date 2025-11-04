using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("posseder")]
    public partial class Posseder
    {
        [Key]
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }
        [Key]
        [Column("id_fonctionalite")]
        public int Id_Fonctionalite { get; set; }
    }
}
