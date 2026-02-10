using Microsoft.EntityFrameworkCore;
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

        [Column("nom_type_client")]
        [MaxLength(50)]
        public string Nom_Type_Client { get; set; }

        [Column("mult_tarif_type_client")]
        [Precision(5, 2)]
        public double Mult_Tarif_Type_Client { get; set; }

        [InverseProperty(nameof(Prise_Contact.Type_ClientNavigation))]
        public virtual ICollection<Prise_Contact> Prise_Contacts { get; set; } = new List<Prise_Contact>();

        [InverseProperty(nameof(Autoriser.Type_ClientNavigation))]
        public virtual ICollection<Autoriser> Autorisers { get; set; } = new List<Autoriser>();
    }
}
