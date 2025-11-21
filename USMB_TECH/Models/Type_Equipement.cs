using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("type_equipement")]
    public partial class Type_Equipement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_type")]
        public int Id_Type_Equipement { get; set; }

        [Column("nom_type")]
        [MaxLength(50)]
        public string Nom_Type { get; set; }

        [InverseProperty(nameof(Equipement.Type_EquipementNavigation))]
        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();
    }
}
