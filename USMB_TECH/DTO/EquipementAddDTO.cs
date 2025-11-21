namespace USMB_TECH.DTO
{
    public class EquipementAddDTO
    {
        public string Nom_Equipement { get; set; }
        public string Num_Immobilisation { get; set; }
        public DateTime Date_Acquisition { get; set; }
        public string Description_Technique { get; set; }
        public double Prix_Achat { get; set; }
        public double Prix_Revient { get; set; }
        public string Nom_Plateforme { get; set; }
        public string Nom_Modele { get; set; }
        public string Nom_Marque { get; set; }
        public string Type_Equipement { get; set; }

        public bool Disponibilité { get; set; }
        public bool Actif { get; set; }
        public bool Autonomie { get; set; }
        public bool Utilisable_Chez_Le_Client { get; set; }

    }
}
