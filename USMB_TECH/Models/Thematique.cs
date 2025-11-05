using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("thematique")]
    public class Thematique
    {
        [Key]
        [Column("id_thematique")]
        public int Id_Thematique { get; set; }

        [Column("id_sous_thematique")]
        public int Id_Sous_Thematique { get; set; }

        [Column("nom_thematique")]
        [MaxLength(50)]
        public string Nom_Thematique { get; set; }
        }
}
