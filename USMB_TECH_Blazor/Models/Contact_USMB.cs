using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Contact_USMB
    {
        public int Id_Contact { get; set; }

        public int Id_Fonction { get; set; }

        public string Nom_Court { get; set; }

        public string Code_RH { get; set; }

        public string Num_Securite_Social { get; set; }

        public string? Nom_Contact { get; set; }

        public string? Prenom_Contact { get; set; }

        public string? Mail { get; set; }

        public string? Telephone { get; set; }


        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();
    }
}
