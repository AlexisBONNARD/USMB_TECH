using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Type_Client
    {
        public int Id_Type_Client { get; set; }

        [MaxLength(50, ErrorMessage = "Le nom Type Client ne doit pas dépasser 50 caractères")]
        public string Nom_Type_Client { get; set; }

        [Range(0, 999.99, ErrorMessage = "Le Multiplicateur Tarif Type Client doit avoir 5 chiffres max dont 2 max après la virgule")]
        [Precision(5, 2)]
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
