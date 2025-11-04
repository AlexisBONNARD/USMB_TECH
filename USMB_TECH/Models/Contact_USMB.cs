using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("Contact_USMB")]
    public partial class Contact_USMB
    {
        [Key]
        [Column("id_Contact")]
        public int Id_Contact { get; set; }

        [Column("Id_Fonction")]
        public int Id_Fonction { get; set; }

        [Column("Nom_Court")]
        [StringLength(25)]
        public string Nom_Court { get; set; }

        [Column("Code_RH")]
        [StringLength(25)]
        public string Code_RH { get; set; }

        [Column("Num_Securite_Social")]
        [StringLength(13)]
        public char Num_Securite_Social { get; set; }

        [Column("Nom_Contact")]
        [StringLength(50)]
        public string? Nom_Contact { get; set; }

        [Column("Prenom_Contact")]
        [StringLength(50)]
        public string? Prenom_Contact { get; set; }

        [Column("Mail")]
        [StringLength(50)]
        public string? Mail { get; set; }

        [Column("Telephone")]
        [StringLength(50)]
        public string? Telephone { get; set; }
    }
}
