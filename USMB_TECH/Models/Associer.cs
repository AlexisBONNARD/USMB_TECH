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

        [ForeignKey("Id_Plateforme")]
        [InverseProperty(nameof(Plateforme.Associers))]
        public virtual Plateforme? PlateformeNavigation { get; set; } = null!;

        [ForeignKey("Id_Contact")]
        [InverseProperty(nameof(Contact_USMB.Associers))]
        public virtual Contact_USMB? Contact_USMBNavigation { get; set; } = null!;
    }
}
