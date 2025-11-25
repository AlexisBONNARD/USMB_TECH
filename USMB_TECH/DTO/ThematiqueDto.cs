using System;

namespace USMB_TECH.DTO
{
    public class ThematiqueDto
    {
        public int Id_Thematique { get; set; }
        public string? Nom_Thematique { get; set; }
        public int? Id_Sous_Thematique { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ThematiqueDto DTO &&
                    Id_Thematique == DTO.Id_Thematique &&
                    Nom_Thematique == DTO.Nom_Thematique &&
                    Id_Sous_Thematique == DTO.Id_Sous_Thematique;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id_Thematique);
            hash.Add(Nom_Thematique);
            hash.Add(Id_Sous_Thematique);
            return hash.ToHashCode();
        }
    }
}