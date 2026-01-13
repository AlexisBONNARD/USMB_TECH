using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("posseder")]
    public partial class Posseder
    {
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_fonctionnalite")]
        public int Id_Fonctionnalite { get; set; }

        [ForeignKey("Id_Equipement")]
        [InverseProperty(nameof(Equipement.Posseders))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;
    
        [ForeignKey("Id_Fonctionnalite")]
        [InverseProperty(nameof(Fonctionnalite.Posseders))]
        public virtual Fonctionnalite? FonctionnaliteNavigation { get; set; } = null!;
    }
}
