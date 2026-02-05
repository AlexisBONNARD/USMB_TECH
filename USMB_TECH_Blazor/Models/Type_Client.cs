using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Type_Client
    {
        public int Id_Type_Client { get; set; }

        public string Nom_Type_Client { get; set; }

        public double Mult_Tarif_Type_Client { get; set; }

        public virtual ICollection<Prise_Contact> Prise_Contacts { get; set; } = new List<Prise_Contact>();

        public virtual ICollection<Autoriser> Autorisers { get; set; } = new List<Autoriser>();
    }

    public class TypeClientSelection
    {
        public int Id_Type_Client { get; set; }
        public string Nom_Type_Client { get; set; }
        public bool IsSelected { get; set; }
    }
}
