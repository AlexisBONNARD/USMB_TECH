using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("equipement")]
    public partial class Equipement
    {
        [Key]
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("id_marque")]
        public int Id_Marque { get; set; }

        [Column("id_type_equipement")]  
        public int Id_Type_Equipement { get; set; }

        [Column("nom_equipement")]
        [MaxLength(50)]
        public string Nom_Equipement { get; set; }

        [Column("num_equipement")]
        [MaxLength(50)]
        public string Num_Immobilisation { get; set; }

        [Column("date_acquisition", TypeName = "date")]
        public DateTime Date_Acquisition { get; set; }

        [Column("prix_achat")]
        [Precision(10, 2)]
        public double Prix_Achat { get; set; }

        [Column("prix_revient")]
        [Precision(10, 2)]
        public double Prix_Revient { get; set; }

        [Column("description_equipement")]
        [MaxLength(500)]
        public string Description_Technique { get; set; }

        [Column("disponibilite")]
        public bool Disponibilite { get; set; }

        [Column("autonomie")]
        public bool Autonomie { get; set; }

        [Column("utiliser_chez_le_client")]
        public bool Utiliser_Chez_Le_Client { get; set; }

        [Column("actif")]
        public bool Actif { get; set; }
    }
}
