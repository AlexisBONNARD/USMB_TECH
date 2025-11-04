using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("referencer")]
    public partial class Referencer
    {
        [Key]
        [Column("id_contact")]
        public int Id_Contact { get; set; }

        [Key]
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("role")]
        public string Role { get; set; }
    }
}
