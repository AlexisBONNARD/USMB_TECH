using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("prise_contact")]
    public partial class Prise_Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("num_prise_contact")]
        public int Num_Prise_Contact { get; set; }

        [Column("nom_court")]
        public string? Nom_Court { get; set; }

        [Column("id_equipement")]
        public int? Id_Equipement { get; set; }

        [Column("id_pole_expertise")]
        public int? Id_Pole_Expertise { get; set; }

        [Column("id_type_client")]
        public int Id_Type_Client { get; set; }

        [Column("nom_contact")]
        [MaxLength(50)]
        public string Nom_Contact { get; set; }

        [Column("prenom_contact")]
        [MaxLength(50)]
        public string Prenom_Contact { get; set; }

        [Column("entreprise_contact")]
        [MaxLength(100)]
        public string Entreprise_Contact { get; set; }

        [Column("email_contact")]
        [MaxLength(60)]
        public string Email_Contact { get; set; }

        [Column("description_besoins")]
        [MaxLength(200)]
        public string Description_besoins { get; set; }

        [ForeignKey("Nom_Court")]
        [InverseProperty(nameof(Laboratoire.Prise_Contacts))]
        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        [ForeignKey("Id_Equipement")]
        [InverseProperty(nameof(Equipement.Prise_Contacts))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        [ForeignKey("Id_Pole_Expertise")]
        [InverseProperty(nameof(Pole_Expertise.Prise_Contacts))]
        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

        [ForeignKey("Id_Type_Client")]
        [InverseProperty(nameof(Type_Client.Prise_Contacts))]
        public virtual Type_Client? Type_ClientNavigation { get; set; } = null!;
    }
}
