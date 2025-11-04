using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("presenter")]
    public class Presenter
    {
        [Key]
        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Key]
        [Column("id_prestation")]
        public int Id_Prestation { get; set; }
    }
}
