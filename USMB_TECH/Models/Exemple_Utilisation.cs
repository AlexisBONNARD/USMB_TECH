using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("exemple_utilisation")]
    public class Exemple_Utilisation
    {
        [Key]
        [Column("id_exemple_utilisation")]
        public int Id_Exemple_Utilisation { get; set; }

        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("nom_utilisation")]
        [MaxLength(50)]
        public string Nom_Utilisation { get; set; }

        [Column("description_utilisation")]
        [MaxLength(500)]
        public string Description_Utilisation { get; set; }
    }
}
