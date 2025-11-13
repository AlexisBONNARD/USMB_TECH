using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH.Models;

namespace USMB_TECH_Blazor.Models
{
    public class Equipement
    {
        public int Id_Equipement { get; set; }

        public int Id_Plateforme { get; set; }

        public int Id_Modele { get; set; }

        public int Id_Type_Equipement { get; set; }

        public string Nom_Equipement { get; set; }

        public string Num_Immobilisation { get; set; }

        public DateTime Date_Acquisition { get; set; }

        public double Prix_Achat { get; set; }

        public double Prix_Revient { get; set; }

        public string Description_Technique { get; set; }

        public bool Disponibilite { get; set; }

        public bool Autonomie { get; set; }

        public bool Utilisable_Chez_Le_Client { get; set; }

        public bool Actif { get; set; }

        public virtual ICollection<Exemple_Utilisation> Exemple_Utilisations { get; set; } = new List<Exemple_Utilisation>();
    }
}