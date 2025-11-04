using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    public class Est_Lier
    {
        [Column("Nom_Court")]
        [StringLength(25)]
        public string Nom_Court { get; set; }

        [Column("Id_Thematique")]
        public int Id_Thematique { get; set; }
    }
}
