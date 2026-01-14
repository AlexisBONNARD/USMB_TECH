using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace USMB_TECH.Models
{
    [Table("pole_expertise")]
    public partial class Pole_Expertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_pole_expertise")]
        public int Id_Pole_Expertise { get; set; }

        [Column("id_domaine_excellence")]
        public int Id_Domaine_Excellence { get; set; }

        [Column("nom_pole_expertise")]
        [MaxLength(50)]
        public string Nom_Pole_Expertise { get; set; }

        [Column("description_pole_expertise")]
        [MaxLength(250)]
        public string Description_Pole_Expertise { get; set; }

        [Column("actif")]
        public bool Actif {  get; set; }

        [ForeignKey("Id_Domaine_Excellence")]
        [InverseProperty(nameof(Domaine_Excellence.Pole_Expertises))]
        public virtual Domaine_Excellence? Domaine_ExcellenceNavigation { get; set; } = null!;

        [InverseProperty(nameof(Presenter.Pole_ExpertiseNavigation))]
        public virtual ICollection<Presenter> Presenters { get; set; } = new List<Presenter>();

        [InverseProperty(nameof(Equipement.Pole_ExpertiseNavigation))]
        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();

        [InverseProperty(nameof(Specifier.Pole_ExpertiseNavigation))]
        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();

        [InverseProperty(nameof(Prise_Contact.Pole_ExpertiseNavigation))]
        public virtual ICollection<Prise_Contact> Prise_Contacts { get; set; } = new List<Prise_Contact>();

        [InverseProperty(nameof(Gerer.Pole_ExpertiseNavigation))]
        public virtual ICollection<Gerer> Gerers { get; set; } = new List<Gerer>();

        [InverseProperty(nameof(Associer.Pole_ExpertiseNavigation))]
        public virtual ICollection<Associer> Associers { get; set; } = new List<Associer>();

        [InverseProperty(nameof(Exemple_Utilisation.Pole_ExpertiseNavigation))]
        public virtual ICollection<Exemple_Utilisation> Exemple_Utilisations { get; set; } = new List<Exemple_Utilisation>();

        [InverseProperty(nameof(Photo.Pole_ExpertiseNavigation))]
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    }
}
