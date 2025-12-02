using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("associer")]
    public partial class Associer
    {
        [Column("id_pole_expertise")]
        public int Id_Pole_Expertise { get; set; }

        [Column("id_contact")]
        public int Id_Contact { get; set; }

        [Column("fonction")]
        [MaxLength(50)]
        public string Fonction { get; set; }

        [ForeignKey("Id_Pole_Expertise")]
        [InverseProperty(nameof(Pole_Expertise.Associers))]
        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

        [ForeignKey("Id_Contact")]
        [InverseProperty(nameof(Contact_USMB.Associers))]
        public virtual Contact_USMB? Contact_USMBNavigation { get; set; } = null!;
    }
}
