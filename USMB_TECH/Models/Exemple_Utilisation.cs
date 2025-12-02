using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("exemple_utilisation")]
    public class Exemple_Utilisation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_exemple_utilisation")]
        public int Id_Exemple_Utilisation { get; set; }

        [Column("id_equipement")]
        public int? Id_Equipement { get; set; }

        [Column("id_pole_expertise")]
        public int? Id_Pole_Expertise { get; set; }

        [Column("nom_utilisation")]
        [MaxLength(50)]
        public string Nom_Utilisation { get; set; }

        [Column("description_utilisation")]
        [MaxLength(500)]
        public string Description_Utilisation { get; set; }

        [ForeignKey("Id_Equipement")]
        [InverseProperty(nameof(Equipement.Exemple_Utilisations))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        [ForeignKey("Id_Pole_Expertise")]
        [InverseProperty(nameof(Pole_Expertise.Exemple_Utilisations))]
        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

    }
}
