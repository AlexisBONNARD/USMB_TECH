using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH.Models;

namespace USMB_TECH_Blazor.Models
{
    public class Equipement
    {
        public int Id_Equipement { get; set; }

        public int Id_Pole_Expertise { get; set; }

        public int Id_Modele { get; set; }

        public int Id_Type_Equipement { get; set; }

        [Required(ErrorMessage = "Le nom de l'équipement est obligatoire")]
        public string Nom_Equipement { get; set; }
        [Required(ErrorMessage = "Le numéro d'immobilisation est obligatoire ou a été mal écrit")]
        public string Num_Immobilisation { get; set; }
        public DateTime Date_Acquisition { get; set; } = DateTime.Now;

        public string Description_Technique { get; set; }

        [Required(ErrorMessage = "Un prix d'achat est obligatoire")]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix d'achat ne peut pas être négatif")]
        public double Prix_Achat { get; set; }

        [Required(ErrorMessage = "Un prix de revient est obligatoire")]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix de revient ne peut pas être négatif")]
        public double Prix_Revient { get; set; }

        [Required(ErrorMessage = "Une pole_expertise doit être obligatoirement associée")]
        public string Nom_Pole_Expertise { get; set; }


        [Required(ErrorMessage = "Un modèle doit être obligatoirement associé")]
        public string Nom_Modele { get; set; }

        [Required(ErrorMessage = "Une marque doit être obligatoirement associée")]
        public string Nom_Marque { get; set; }

        [Required(ErrorMessage = "Un type d'équipement doit être obligatoirement associé")]
        public string Type_Equipement { get; set; }

        public bool Autonomie { get; set; }

        public bool Utilisable_Chez_Le_Client { get; set; }

        public bool Actif { get; set; } = true;
        public bool Disponibilite { get; set; }

        public string Nom_Contact { get; set; }

        public string Nom_Exemple { get; set; }

        public string Description_Exemple { get; set; }

        public virtual ICollection<Exemple_Utilisation> Exemple_Utilisations { get; set; } = new List<Exemple_Utilisation>();

        public virtual ICollection<Fournir> Fournirs { get; set; } = new List<Fournir>();

        public Modele? ModeleNavigation { get; set; }
        
        public virtual Marque Marque_Equipement { get; set; } = new Marque();

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public Pole_Expertise? Pole_ExpertiseNavigation { get; set; }

        public Type_Equipement? Type_EquipementNavigation { get; set; }
    }
}