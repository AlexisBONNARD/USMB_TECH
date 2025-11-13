using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("fournir")]
    public partial class Fournir
    {
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_prestation")]
        public int Id_Prestation { get; set; }

        [ForeignKey("id_equipement")]
        [InverseProperty(nameof(Equipement.Fournirs))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        [ForeignKey("id_prestation")]
        [InverseProperty(nameof(Prestation.Fournirs))]
        public virtual Prestation? PrestationNavigation { get; set; } = null!;
    }
}
