using System;

namespace USMB_TECH.DTO
{
    public class PrestationDTO
    {
        public int Id_Prestation { get; set; }
        public int Id_Unite_Oeuvre { get; set; }
        public int Id_Type_Prestation { get; set; }
        public string Nom_Court { get; set; }
        public int Id_Contact { get; set; }
        public string Intitule_Prestation { get; set; }
        public string Description_Prestation { get; set; }
        public double Prix_Revient { get; set; }
        public double Prix_Vente { get; set; }
        public bool Peux_Ce_Realiser_Chez_Le_Client { get; set; }
        public bool Actif { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PrestationDTO DTO &&
                   Id_Prestation == DTO.Id_Prestation &&
                   Id_Unite_Oeuvre == DTO.Id_Unite_Oeuvre &&
                   Id_Type_Prestation == DTO.Id_Type_Prestation &&
                   Nom_Court == DTO.Nom_Court &&
                   Id_Contact == DTO.Id_Contact &&
                   Intitule_Prestation == DTO.Intitule_Prestation &&
                   Description_Prestation == DTO.Description_Prestation &&
                   Prix_Revient == DTO.Prix_Revient &&
                   Prix_Vente == DTO.Prix_Vente &&
                   Peux_Ce_Realiser_Chez_Le_Client == DTO.Peux_Ce_Realiser_Chez_Le_Client &&
                   Actif == DTO.Actif;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id_Prestation);
            hash.Add(Id_Unite_Oeuvre);
            hash.Add(Id_Type_Prestation);
            hash.Add(Nom_Court);
            hash.Add(Id_Contact);
            hash.Add(Intitule_Prestation);
            hash.Add(Description_Prestation);
            hash.Add(Prix_Revient);
            hash.Add(Prix_Vente);
            hash.Add(Peux_Ce_Realiser_Chez_Le_Client);
            hash.Add(Actif);
            return hash.ToHashCode();
        }
    }
}
