using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("prestation")]
    public partial class Prestation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_prestation")]
        public int Id_Prestation { get; set; }

        [Column("id_unite_oeuvre")]
        public int Id_Unite_Oeuvre { get; set; }

        [Column("id_type_prestation")]
        public int Id_Type_Prestation { get; set; }

        [Column("id_domaine_excellence")]
        public int Id_Domaine_Excellence { get; set; }

        [Column("nom_court")]
        [MaxLength(25)]
        public string Nom_Court{ get; set; }

        [Column("id_contact")]
        public int Id_Contact { get; set; }

        [Column("intitule_prestation")]
        [MaxLength(100)]
        public string Intitule_Prestation { get; set; }

        [Column("description_prestation")]
        [MaxLength(500)]
        public string Description_Prestation { get; set; }

        [Column("prix_revient")]
        [Precision(10, 2)]
        public double Prix_Revient { get; set; }

        [Column("prix_vente")]
        [Precision(10, 2)]
        public double Prix_Vente { get; set; }

        [Column("peux_ce_realiser_chez_le_client")]
        public bool Peux_Ce_Realiser_Chez_Le_Client { get; set; }

        [Column("actif")]
        public bool Actif { get; set; }

        [ForeignKey("Id_Unite_Oeuvre")]
        [InverseProperty(nameof(Unite_Oeuvre.Prestations))]
        public virtual Unite_Oeuvre? Unite_OeuvreNavigation { get; set; } = null!;

        [ForeignKey("Id_Type_Prestation")]
        [InverseProperty(nameof(Type_Prestation.Prestations))]
        public virtual Type_Prestation? Type_PrestationNavigation { get; set; } = null!;

        [ForeignKey("Id_Domaine_Excellence")]
        [InverseProperty(nameof(Domaine_Excellence.Prestations))]
        public virtual Domaine_Excellence? Domaine_ExcellenceNavigation { get; set; } = null!;

        [ForeignKey("Nom_Court")]
        [InverseProperty(nameof(Laboratoire.Prestations))]
        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        [ForeignKey("Id_Contact")]
        [InverseProperty(nameof(Contact_USMB.Prestations))]
        public virtual Contact_USMB? Contact_USMBNavigation { get; set; } = null!;

        [InverseProperty(nameof(Presenter.PrestationNavigation))]
        public virtual ICollection<Presenter> Presenters { get; set; } = new List<Presenter>();

        [InverseProperty(nameof(Fournir.PrestationNavigation))]
        public virtual ICollection<Fournir> Fournirs { get; set; } = new List<Fournir>();

        [InverseProperty(nameof(Preciser.PrestationNavigation))]
        public virtual ICollection<Preciser> Precisers { get; set; } = new List<Preciser>();

        [InverseProperty(nameof(Photo.PrestationNavigation))]
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();


    }
}
