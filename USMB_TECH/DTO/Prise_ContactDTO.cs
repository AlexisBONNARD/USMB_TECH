using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class Prise_ContactDTO
    {
        public int Num_Prise_Contact { get; set; }
        public int? Id_Equipement { get; set; }
        public int? Id_Pole_Expertise { get; set; }
        public int Id_Type_Client { get; set; }
        public string Nom_Contact { get; set; }
        public string Prenom_Contact { get; set; }
        public string Entreprise_Contact { get; set; }
        public string Email_Contact { get; set; }
        public string Description_besoins { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Prise_ContactDTO DTO &&
                   Num_Prise_Contact == DTO.Num_Prise_Contact &&
                   Id_Equipement == DTO.Id_Equipement &&
                   Id_Pole_Expertise == DTO.Id_Pole_Expertise &&
                   Id_Type_Client == DTO.Id_Type_Client &&
                   Nom_Contact == DTO.Nom_Contact &&
                   Prenom_Contact == DTO.Prenom_Contact &&
                   Entreprise_Contact == DTO.Entreprise_Contact &&
                   Email_Contact == DTO.Email_Contact &&
                   Description_besoins == DTO.Description_besoins;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Num_Prise_Contact);
            hash.Add(Id_Equipement);
            hash.Add(Id_Pole_Expertise);
            hash.Add(Id_Type_Client);
            hash.Add(Nom_Contact);
            hash.Add(Prenom_Contact);
            hash.Add(Entreprise_Contact);
            hash.Add(Email_Contact);
            hash.Add(Description_besoins);
            return hash.ToHashCode();
        }
    }
}
