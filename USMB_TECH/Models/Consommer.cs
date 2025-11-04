using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("consommer")]
    public partial class Consommer
    {
        [Key]
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Key]
        [Column("id_consommable")]
        public int Id_Consommable { get; set; }

        [Column("quantite")]
        public int Quantite {  get; set; }
    }
}
