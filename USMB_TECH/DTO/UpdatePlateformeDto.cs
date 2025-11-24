using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class UpdatePlateformeDto
    {
        public int Id_Plateforme { get; set; }

        public string Nom_Plateforme { get; set; }
        public string Description_Plateforme { get; set; }
        public string Nom_Contenu { get; set; }
        public string Url_Contenu { get; set; }
        public string Description_Contenu { get; set; }
        public bool Actif { get; set; }

        public List<PhotoDto> Photos { get; set; } = new();
        public List<ExempleUtilisationDto> ExempleUtilisations { get; set; } = new();
        public List<Presenter> Presenters { get; set; } = new();
        public List<MotCleDto> MotsCles { get; set; } = new();
        public List<Thematique> Thematiques { get; set; } = new();
        public List<Equipement> Equipements { get; set; } = new();

        public override bool Equals(object? obj)
        {
            return obj is UpdatePlateformeDto dto &&
                   Id_Plateforme == dto.Id_Plateforme &&
                   Nom_Plateforme == dto.Nom_Plateforme &&
                   Description_Plateforme == dto.Description_Plateforme &&
                   Nom_Contenu == dto.Nom_Contenu &&
                   Url_Contenu == dto.Url_Contenu &&
                   Description_Contenu == dto.Description_Contenu &&
                   Actif == dto.Actif &&
                   EqualityComparer<List<PhotoDto>>.Default.Equals(Photos, dto.Photos) &&
                   EqualityComparer<List<ExempleUtilisationDto>>.Default.Equals(ExempleUtilisations, dto.ExempleUtilisations) &&
                   EqualityComparer<List<Presenter>>.Default.Equals(Presenters, dto.Presenters) &&
                   EqualityComparer<List<MotCleDto>>.Default.Equals(MotsCles, dto.MotsCles) &&
                   EqualityComparer<List<Thematique>>.Default.Equals(Thematiques, dto.Thematiques) &&
                   EqualityComparer<List<Equipement>>.Default.Equals(Equipements, dto.Equipements);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id_Plateforme);
            hash.Add(Nom_Plateforme);
            hash.Add(Description_Plateforme);
            hash.Add(Nom_Contenu);
            hash.Add(Url_Contenu);
            hash.Add(Description_Contenu);
            hash.Add(Actif);
            hash.Add(Photos);
            hash.Add(ExempleUtilisations);
            hash.Add(Presenters);
            hash.Add(MotsCles);
            hash.Add(Thematiques);
            hash.Add(Equipements);
            return hash.ToHashCode();
        }
    }

}
