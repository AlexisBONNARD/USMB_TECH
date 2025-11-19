using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("type_client")]
    public partial class Type_Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_type_client")]
        public int Id_Type_Client { get; set; }

        [Column("nom_typeclient")]
        [MaxLength(50)]
        public string Nom_Type_Client { get; set; }

        [InverseProperty(nameof(Prise_Contact.Type_ClientNavigation))]
        public virtual ICollection<Prise_Contact> Prise_Contacts { get; set; } = new List<Prise_Contact>();
    }
}
