using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("type_prestation")]
    public partial class Type_Prestation
    {
        
        [Column("id_type_prestation")]
        public int Id_Type_Prestation { get; set; }

        [Column("nom_type_prestation")]
        [MaxLength(50)]
        public string Nom_Type_Prestation { get; set; }

        [InverseProperty(nameof(Prestation.Type_PrestationNavigation))]
        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();
    }
}
