using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
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
        [MaxLength(50, ErrorMessage = "Le nom Equipement ne doit pas dépasser 50 caractères")]
        public string Nom_Equipement { get; set; }
        
        [Required(ErrorMessage = "Le numéro d'immobilisation est obligatoire ou a été mal écrit")]
        [MaxLength(50, ErrorMessage = "Le numéro d'Imobilisation ne doit pas dépasser 50 caractères")]
        [RegularExpression(@"[a-zA-Z0-9-]{1,50}", ErrorMessage = "Le numéro d'immobilisation doit contenir uniquement des lettres, chiffres ou tirets")]
        public string Num_Immobilisation { get; set; }

        public DateTime Date_Acquisition { get; set; } = DateTime.Now;

        [Required(ErrorMessage ="La description technique de votre équipement est obligatoire")]
        [MaxLength(500, ErrorMessage = "La Description Technique de l'équipement ne doit pas dépasser 500 caractères")]
        public string Description_Technique { get; set; }

        [Required(ErrorMessage = "Un prix d'achat est obligatoire")]
        [Range(0, 99999999.99, ErrorMessage = "Le prix d'achat ne peut pas être négatif")]
        public double Prix_Achat { get; set; }

        [Required(ErrorMessage = "Un prix de revient est obligatoire")]
        [Range(0, 99999999.99, ErrorMessage = "Le prix de revient ne peut pas être négatif")]
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
        
        [MaxLength(150, ErrorMessage = "L'URL du Model 3D de l'équipement ne doit pas dépasser 150 caractères")]
        public string? Url_Modele_3D { get; set; }

        public string Nom_Contact { get; set; }

        [Required(ErrorMessage = "il est nécessaire de renseigner un Nom pour l'exemple d'utilisation")]
        public string Nom_Exemple { get; set; }

        [Required(ErrorMessage = "l'exemple d'utilisation doit avoir une description")]
        public string Description_Exemple { get; set; }

        public virtual ICollection<string> MotsCles { get; set; } = new List<string>();
        public virtual ICollection<Fonctionnalite> Fonctionnalites { get; set; } = new List<Fonctionnalite>();

        public virtual ICollection<Exemple_Utilisation> Exemple_Utilisations { get; set; } = new List<Exemple_Utilisation>();

        public virtual ICollection<Fournir> Fournirs { get; set; } = new List<Fournir>();

        public Modele? ModeleNavigation { get; set; }
        
        public virtual Marque Marque_Equipement { get; set; } = new Marque();

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public Pole_Expertise? Pole_ExpertiseNavigation { get; set; }

        public Type_Equipement? Type_EquipementNavigation { get; set; }

        public virtual ICollection<Thematique> Thematiques { get; set; } = new List<Thematique>();
        public virtual ICollection<Qualifier> Qualifiers { get; set; } = new List<Qualifier>();
        public virtual ICollection<Posseder> Posseders { get; set; } = new List<Posseder>();
        public virtual ICollection<Autoriser> Autorisers { get; set; } = new List<Autoriser>();
        public virtual ICollection<Proposer> Proposers { get; set; } = new List<Proposer>();

        public Type_Client? Type_ClientNavigation { get; set; } // pas sur des deux dernier

        public Type_Utilisation? Type_UtilisationNavigation { get; set; }
    }
}