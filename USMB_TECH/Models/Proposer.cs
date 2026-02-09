using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("proposer")]
    public partial class Proposer
    {
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_type_utilisation")]
        public int Id_Type_Utilisation { get; set; }

        [ForeignKey("Id_Equipement")]
        [InverseProperty(nameof(Equipement.Proposers))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        [ForeignKey("Id_Type_Utilisation")]
        [InverseProperty(nameof(Type_Utilisation.Proposers))]
        public virtual Type_Utilisation? Type_UtilisationNavigation { get; set; } = null!;
    }
}
