using System.Xml.Linq;

namespace USMB_TECH.DTO
{
    public class PlateformeUpdateDTO
    {   public string? Nom_Plateforme { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PlateformeUpdateDTO DTO &&
                   Nom_Plateforme == DTO.Nom_Plateforme;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nom_Plateforme);
        }
    }
}
