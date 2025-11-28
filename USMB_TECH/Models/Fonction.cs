using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("fonction")]
    public partial class Fonction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_fonction")]
        public int Id_Fonction { get; set; }

        [Column("nom_fonction")]
        [MaxLength(50)]
        public string Nom_Fonction { get; set; }

        [InverseProperty(nameof(Contact_USMB.FonctionNavigation))]
        public virtual ICollection<Contact_USMB> Contacts { get; set; } = new List<Contact_USMB>();
    }
}
