using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("associer")]
    public partial class Associer
    {
        [Key]
        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("id_contact")]
        public int Id_Contact { get; set; }

        [Column("fonction")]
        [MaxLength(50)]
        public string Fonction { get; set; }
    }
}
