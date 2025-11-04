using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("designer")]
    public class Designer
    {
        [Key]
        [Column("id_mot_clef")]
        public int Id_Mot_Clef { get; set; }

        [Key]
        [Column("nom_court")]
        public int Nom_Court { get; set; }
    }
}
