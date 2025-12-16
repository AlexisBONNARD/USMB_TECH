using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH.Models;

namespace USMB_TECH_Blazor.Models
{
    public class Prestation
    {
        public int Id_Prestation { get; set; }

        public int Id_Unite_Oeuvre { get; set; }

        public int Id_Type_Prestation { get; set; }

        public string? Nom_Court { get; set; }

        public string Nom_Pole_Expertise { get; set; }

        [Required(ErrorMessage = "Un domaine d'expertise est obligatoire")]
        public string Nom_Domaine { get; set; }

        [Required(ErrorMessage = "Une prestation doit obligatoirement avoir un type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Un unité de mesure de temps doit être attribué")]
        public string Unite { get; set; }

        public int Id_Contact { get; set; }

        [Required(ErrorMessage = "Le nom du contact doit être renseigné")]
        public string Nom_Contact  { get; set; }

        [Required(ErrorMessage = "La prestation actuelle n'a pas reçu de nom")]
        public string Intitule_Prestation { get; set; }

        [Required(ErrorMessage = "Une description doit être donnée pour la prestation")]
        public string Description_Prestation { get; set; }

        [Required(ErrorMessage = "Vous devez renseigner un prix de revient")]
        public double Prix_Revient { get; set; }

        [Required(ErrorMessage = "Vous devez renseigner un prix de vente")]
        public double Prix_Vente { get; set; }

        public bool Peux_Ce_Realiser_Chez_Le_Client { get; set; }

        public bool Actif { get; set; } = true;

        [Required(ErrorMessage = "Il est nécessaire de sélectionner au moins un mot-clé")]
        public ICollection<string> Mots_Clefs { get; set; } = new List<string>();

        public ICollection<string> Poles { get; set; } = new List<string>();

        public virtual ICollection<Presenter> Presenters { get; set; } = new List<Presenter>();

        public virtual ICollection<Fournir> Fournirs { get; set; } = new List<Fournir>();

        public virtual ICollection<Preciser> Precisers { get; set; } = new List<Preciser>();

        public virtual Type_Prestation? Type_PrestationNavigation { get; set; } = null!;

        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
