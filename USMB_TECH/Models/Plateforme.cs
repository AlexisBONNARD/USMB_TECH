using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("plateforme")]
    public partial class Plateforme
    {
        [Key]
        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("nom_plateforme")]
        [MaxLength(50)]
        public string Nom_Plateforme { get; set; }

        [Column("description_plateforme")]
        [MaxLength(50)]
        public string Description_Plateforme { get; set; }

        [Column("nom_contenu")]
        [MaxLength(50)]
        public string Nom_Contenu { get; set; } 

        [Column("url_contenu")]
        [MaxLength(150)]
        public string Url_Contenu { get; set; }

        [Column("description_contenu")]
        [MaxLength(500)]
        public string Description_Contenu { get; set; }

        [Column("actif")]
        public bool Actif {  get; set; }

        [InverseProperty(nameof(Presenter.PlateformeNavigation))]
        public virtual ICollection<Presenter> Presenters { get; set; } = new List<Presenter>();

        [InverseProperty(nameof(Equipement.PlateformeNavigation))]
        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();

        [InverseProperty(nameof(Specifier.PlateformeNavigation))]
        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();

        [InverseProperty(nameof(Prise_Contact.PlateformeNavigation))]
        public virtual ICollection<Prise_Contact> Prise_Contacts { get; set; } = new List<Prise_Contact>();

        [InverseProperty(nameof(Gerer.PlateformeNavigation))]
        public virtual ICollection<Gerer> Gerers { get; set; } = new List<Gerer>();

        [InverseProperty(nameof(Associer.PlateformeNavigation))]
        public virtual ICollection<Associer> Associers { get; set; } = new List<Associer>();

        [InverseProperty(nameof(Exposer.PlateformeNavigation))]
        public virtual ICollection<Exposer> Exposers { get; set; } = new List<Exposer>();
    }
}
