using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("consommer")]
    public partial class Consommer
    {
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_consommable")]
        public int Id_Consommable { get; set; }

        [Column("quantite")]
        public int Quantite {  get; set; }

        [ForeignKey("Id_Equipement")]
        [InverseProperty(nameof(Equipement.Consommers))]
        public virtual Equipement EquipementNavigation { get; set; } = null!;

        [ForeignKey("Id_Consommable")]
        [InverseProperty(nameof(Consommable.Consommers))]
        public virtual Consommable ConsommableNavigation { get; set; } = null!;

    }
}
