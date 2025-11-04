using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("type_client")]
    public partial class Type_Client
    {
        [Key]
        [Column("id_type_client")]
        public int Id_Type_Client { get; set; }

        [Column("nom_typeclient")]
        [MaxLength(50)]
        public string Nom_Type_Client { get; set; }
    }
}
