using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("specifier")]
    public class Specifier
    {
        [Key]
        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Key]
        [Column("id_mot_clef")]
        public int Id_Mot_Clef { get; set; }
    }
}
