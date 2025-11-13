using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("referencer")]
    public partial class Referencer
    {
        [Column("id_contact")]
        public int Id_Contact { get; set; }

        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [ForeignKey("id_contact")]
        [InverseProperty(nameof(Contact_USMB.Referencers))]
        public virtual Contact_USMB? Contact_USMBNavigation { get; set; } = null!;

        [ForeignKey("id_equipement")]
        [InverseProperty(nameof(Equipement.Referencers))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;
    }
}
