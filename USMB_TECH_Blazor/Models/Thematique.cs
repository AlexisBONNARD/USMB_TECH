using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Thematique
    {
        public int Id_Thematique { get; set; }

        public int Id_Sous_Thematique { get; set; }

        public string Nom_Thematique { get; set; }
    }
}
