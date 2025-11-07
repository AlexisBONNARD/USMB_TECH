namespace USMB_TECH.DTO
{
    public class PlateformeDTO
    {
        public int Id_Plateforme { get; set; }
        public string Nom_Plateforme { get; set; }
        public string Description_Plateforme { get; set; }
        public string Nom_Contenu { get; set; }
        public string Url_Contenu { get; set; }
        public string Description_Contenu { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PlateformeDTO DTO &&
                   Id_Plateforme == DTO.Id_Plateforme &&
                   Nom_Plateforme == DTO.Nom_Plateforme &&
                   Description_Plateforme == DTO.Description_Plateforme &&
                   Nom_Contenu == DTO.Nom_Contenu &&
                   Url_Contenu == DTO.Url_Contenu &&
                   Description_Contenu == DTO.Description_Contenu;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id_Plateforme, Nom_Plateforme, Description_Plateforme, Nom_Contenu, Url_Contenu, Description_Contenu);
        }
    }
}
