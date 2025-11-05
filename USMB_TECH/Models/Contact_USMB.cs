using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("contact_usmb")]
    public partial class Contact_USMB
    {
        [Key]
        [Column("id_Contact")]
        public int Id_Contact { get; set; }

        [Column("id_fonction")]
        public int Id_Fonction { get; set; }

        [Column("nom_court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("code_rh")]
        [MaxLength(25)]
        public string Code_RH { get; set; }

        [Column("num_securite_social")]
        [StringLength(13)]
        public string Num_Securite_Social { get; set; }

        [Column("nom_contact")]
        [MaxLength(50)]
        public string? Nom_Contact { get; set; }

        [Column("prenom_contact")]
        [MaxLength(50)]
        public string? Prenom_Contact { get; set; }

        [Column("mail")]
        [MaxLength(50)]
        public string? Mail { get; set; }

        [Column("telephone")]
        [MaxLength(50)]
        public string? Telephone { get; set; }

        [InverseProperty(nameof(Prestation.Contact_USMBNavigation))]
        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();

        [ForeignKey("id_fonction")]
        [InverseProperty(nameof(Fonction.Contacts))]
        public virtual Fonction? FonctionNavigation { get; set; } = null!;

        [ForeignKey("id_fonction")]
        [InverseProperty(nameof(Laboratoire.Contacts))]
        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;
    }
}
