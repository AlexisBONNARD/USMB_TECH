using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("est_Lier")]
    public partial class Est_Lier
    {
        [Column("Nom_Court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("Id_Thematique")]
        public int Id_Thematique { get; set; }
    }
}
