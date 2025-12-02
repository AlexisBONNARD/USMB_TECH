using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;
using System.Xml.Linq;

namespace USMB_TECH.DTO
{
    public class EquipementDTO
    {
        public int Id_Equipement { get; set; }
        public int Id_Pole_Expertise { get; set; }
        public int Id_Modele { get; set; }
        public int Id_Type_Equipement { get; set; }
        public string Nom_Equipement { get; set; }
        public string Num_Immobilisation { get; set; }
        public DateTime Date_Acquisition { get; set; }
        public double Prix_Achat { get; set; }
        public double Prix_Revient { get; set; }
        public string Description_Technique { get; set; }
        public bool Disponibilite { get; set; }
        public bool Autonomie { get; set; }
        public bool Utilisable_Chez_Le_Client { get; set; }
        public bool Actif { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is EquipementDTO DTO &&
                    Id_Equipement == DTO.Id_Equipement &&
                    Id_Pole_Expertise == DTO.Id_Pole_Expertise &&
                    Id_Modele == DTO.Id_Modele &&
                    Id_Type_Equipement == DTO.Id_Type_Equipement &&
                    Nom_Equipement == DTO.Nom_Equipement &&
                    Num_Immobilisation == DTO.Num_Immobilisation &&
                    Date_Acquisition == DTO.Date_Acquisition &&
                    Prix_Achat == DTO.Prix_Achat &&
                    Prix_Revient == DTO.Prix_Revient &&
                    Description_Technique == DTO.Description_Technique &&
                    Disponibilite == DTO.Disponibilite &&
                    Autonomie == DTO.Autonomie &&
                    Utilisable_Chez_Le_Client == DTO.Utilisable_Chez_Le_Client &&
                    Actif == DTO.Actif;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id_Equipement );
            hash.Add(Id_Pole_Expertise );
            hash.Add(Id_Modele );
            hash.Add(Id_Type_Equipement );
            hash.Add(Nom_Equipement );
            hash.Add(Num_Immobilisation );
            hash.Add(Date_Acquisition );
            hash.Add(Prix_Achat );
            hash.Add(Prix_Revient );
            hash.Add(Description_Technique );
            hash.Add(Disponibilite );
            hash.Add(Autonomie );
            hash.Add(Utilisable_Chez_Le_Client );
            hash.Add(Actif);
            return hash.ToHashCode();
        }
    }
}
