using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("autoriser")]
    public partial class Autoriser
    {
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_type_client")]
        public int Id_Type_Client { get; set; }

        [ForeignKey("Id_Equipement")]
        [InverseProperty(nameof(Equipement.Autorisers))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        [ForeignKey("Id_Type_Client")]
        [InverseProperty(nameof(Type_Client.Autorisers))]
        public virtual Type_Client? Type_ClientNavigation { get; set; } = null!;
    }
}
