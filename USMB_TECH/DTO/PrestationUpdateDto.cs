namespace USMB_TECH.DTO
{
    public class PrestationUpdateDto
    {
        public int Id_Prestation { get; set; }


        public string Intitule_Prestation { get; set; }
        public string Description_Prestation { get; set; }
        public string Nom_Court { get; set; }
        public bool Actif { get; set; }

        public string UniteOeuvre { get; set; }

        public int Id_Type_Prestation { get; set; }
        public int Id_Domaine_Excellence { get; set; }
        public List<int> ContactIds { get; set; } = new();
        public List<int> MotCleIds { get; set; } = new();
    }

}
