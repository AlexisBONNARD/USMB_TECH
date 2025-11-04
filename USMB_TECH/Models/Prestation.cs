using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("prestation")]
    public partial class Prestation
    {
        [Key]
        [Column("id_prestation")]
        public int Id_Prestation { get; set; }

        [Column("id_unite_oeuvre")]
        public int Id_Unite_Oeuvre { get; set; }

        [Column("id_type_prestation")]
        public int Id_Type_Prestation { get; set; }

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

        [ForeignKey("id_unite_oeuvre")]
        [InverseProperty(nameof(Unite_Oeuvre.Prestations))]
        public virtual Unite_Oeuvre? Unite_OeuvreNavigation { get; set; } = null!;

        [ForeignKey("id_type_prestation")]
        [InverseProperty(nameof(Type_Prestation.Prestations))]
        public virtual Type_Prestation? Type_PrestationNavigation { get; set; } = null!;

        [ForeignKey("nom_court")]
        [InverseProperty(nameof(Laboratoire.Prestations))]
        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        [ForeignKey("id_contact")]
        [InverseProperty(nameof(Contact_USMB.Prestations))]
        public virtual Contact_USMB? Contact_USMBNavigation { get; set; } = null!;
    }
}
