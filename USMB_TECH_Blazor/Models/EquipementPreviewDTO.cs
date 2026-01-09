namespace USMB_TECH_Blazor.Models
{
    public class EquipementPreviewDTO
    {
        public int Id_Equipement { get; set; }
        public string Nom_Equipement { get; set; }
        public string Description_Technique { get; set; }
        public string Nom_Pole_Expertise { get; set; }
        public double Prix_Achat { get; set; }
        public DateTime Date_Acquisition { get; set; }
        public bool Disponibilite { get; set; }
        public string Url_Modele_3D { get; set; }

        public List<string> MotsCles { get; set; } = new();
        public List<string> Thematiques { get; set; } = new();
    }

}
