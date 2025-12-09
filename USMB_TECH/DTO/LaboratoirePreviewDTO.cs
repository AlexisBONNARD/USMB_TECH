namespace USMB_TECH.DTO
{
    public class LaboratoirePreviewDTO
    {
        public string Nom_Court { get; set; }
        public string Nom_Long { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public List<string> MotsCles { get; set; } = new();
        public List<string> Thematiques { get; set; } = new();
    }
}
