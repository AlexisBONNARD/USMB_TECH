using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class UpdateEquipementDto
    {
        public int Id_Equipement { get; set; }          // obligatoire pour identifier l'équipement
        public int Id_Plateforme { get; set; }          // rattachement à la plateforme
        public int Id_Modele { get; set; }              // rattachement au modèle
        public int Id_Type_Equipement { get; set; }     // rattachement au type

        public string Nom_Equipement { get; set; }      // nom de l'équipement
        public string Num_Immobilisation { get; set; }  // ⚠️ obligatoire (NOT NULL en base)
        public DateTime Date_Acquisition { get; set; }  // date d'achat
        public string Description_Technique { get; set; }

        public decimal Prix_Achat { get; set; }
        public decimal Prix_Revient { get; set; }

        public bool Disponibilite { get; set; }
        public bool Autonomie { get; set; }
        public bool Utilisable_Chez_Le_Client { get; set; }
        public bool Actif { get; set; }

        public string? Nom_Plateforme { get; set; }
        public string? Nom_Modele { get; set; }
        public string? Nom_Marque { get; set; }
        public string? Type_Equipement { get; set; }

        public List<PhotoDto> Photos { get; set; } = new();
        public List<ExempleUtilisationDto> Exemple_Utilisations { get; set; } = new();
        public List<Fournir> Fournirs { get; set; } = new();

        public override bool Equals(object? obj)
        {
            return obj is UpdateEquipementDto dto &&
                   Id_Equipement == dto.Id_Equipement &&
                   Id_Plateforme == dto.Id_Plateforme &&
                   Id_Modele == dto.Id_Modele &&
                   Id_Type_Equipement == dto.Id_Type_Equipement &&
                   Nom_Equipement == dto.Nom_Equipement &&
                   Num_Immobilisation == dto.Num_Immobilisation &&
                   Date_Acquisition == dto.Date_Acquisition &&
                   Description_Technique == dto.Description_Technique &&
                   Prix_Achat == dto.Prix_Achat &&
                   Prix_Revient == dto.Prix_Revient &&
                   Disponibilite == dto.Disponibilite &&
                   Autonomie == dto.Autonomie &&
                   Utilisable_Chez_Le_Client == dto.Utilisable_Chez_Le_Client &&
                   Actif == dto.Actif &&
                   Nom_Plateforme == dto.Nom_Plateforme &&
                   Nom_Modele == dto.Nom_Modele &&
                   Nom_Marque == dto.Nom_Marque &&
                   Type_Equipement == dto.Type_Equipement &&
                   EqualityComparer<List<PhotoDto>>.Default.Equals(Photos, dto.Photos) &&
                   EqualityComparer<List<ExempleUtilisationDto>>.Default.Equals(Exemple_Utilisations, dto.Exemple_Utilisations) &&
                   EqualityComparer<List<Fournir>>.Default.Equals(Fournirs, dto.Fournirs);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id_Equipement);
            hash.Add(Id_Plateforme);
            hash.Add(Id_Modele);
            hash.Add(Id_Type_Equipement);
            hash.Add(Nom_Equipement);
            hash.Add(Num_Immobilisation);
            hash.Add(Date_Acquisition);
            hash.Add(Description_Technique);
            hash.Add(Prix_Achat);
            hash.Add(Prix_Revient);
            hash.Add(Disponibilite);
            hash.Add(Autonomie);
            hash.Add(Utilisable_Chez_Le_Client);
            hash.Add(Actif);
            hash.Add(Nom_Plateforme);
            hash.Add(Nom_Modele);
            hash.Add(Nom_Marque);
            hash.Add(Type_Equipement);
            hash.Add(Photos);
            hash.Add(Exemple_Utilisations);
            hash.Add(Fournirs);
            return hash.ToHashCode();
        }
    }
}
