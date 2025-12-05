using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("laboratoire")]
    public partial class Laboratoire
    {
        [Key]
        [Column("nom_court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("id_adresse_campus")]
        public int Id_Adresse_Campus { get; set; }

        [Column("id_adresse_labo")]
        public int Id_Adresse_Labo { get; set; }

        [Column("nom_long")]
        [MaxLength(255)]
        public string Nom_Long { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("Id_Adresse_Campus")]
        [InverseProperty(nameof(Adresse.Laboratoires_campus))]
        public virtual Adresse? Adresse_campusNavigation { get; set; } = null!;

        [ForeignKey("Id_Adresse_Labo")]
        [InverseProperty(nameof(Adresse.Laboratoires_labo))]
        public virtual Adresse? Adresse_laboNavigation { get; set; } = null!;

        [InverseProperty(nameof(Prestation.LaboratoireNavigation))]
        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();

        [InverseProperty(nameof(Contact_USMB.LaboratoireNavigation))]
        public virtual ICollection<Contact_USMB> Contacts { get; set; } = new List<Contact_USMB>();

        [InverseProperty(nameof(Designer.LaboratoireNavigation))]
        public virtual ICollection<Designer> Designers { get; set; } = new List<Designer>();

        [InverseProperty(nameof(Est_Lier.LaboratoireNavigation))]
        public virtual ICollection<Est_Lier> Est_Liers { get; set; } = new List<Est_Lier>();

        [InverseProperty(nameof(Gerer.LaboratoireNavigation))]
        public virtual ICollection<Gerer> Gerers { get; set; } = new List<Gerer>();

        [InverseProperty(nameof(Photo.LaboratoireNavigation))]
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
