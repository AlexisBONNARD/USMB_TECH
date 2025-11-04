using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("mot_clef")]
    public partial class Mot_Clef
    {
        [Key]
        [Column("id_mot_clef")]
        public int Id_Mot_Clef { get; set; }

        [Column("nom_mot_clef")]
        [MaxLength(25)]
        public string Nom_Mot_Clef { get; set; }
    }
}
