using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("Laboratoire")]
    public partial class Laboratoire
    {
        [Key]
        [Column("nom_court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("id_adresse_campus")]
        public int Id_Adresse_Campus { get; set; }

        [Column("Id_Adresse_Labo")]
        public int Id_Adresse_Labo { get; set; }

        [Column("Nom_Long")]
        [MaxLength(255)]
        public string Nom_Long { get; set; }

        [Column("Description")]
        [MaxLength(1000)]
        public string Description { get; set; }


    }
}
