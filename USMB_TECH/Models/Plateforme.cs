namespace USMB_TECH.Models
{
    public partial class Plateforme
    {
        public int Id_Plateforme { get; set; }

        public string Nom_Plateforme { get; set; }

        public string Description_Plateforme { get; set; }

        public string Nom_Contenu { get; set; } 

        public string Url_Contenu { get; set; }

        public string Description_Contenu { get; set; }

        public bool Actif {  get; set; }
    }
}
