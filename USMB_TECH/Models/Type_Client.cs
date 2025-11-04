using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("Type_Client")]
    public class Type_Client
    {
        [Key]
        [Column("Id_TypeClient")]
        public int Id_TypeClient { get; set; }

        [Column("Nom_TypeClient")]
        [StringLength(50)]
        public string Nom_TypeClient { get; set; }
    }
}
