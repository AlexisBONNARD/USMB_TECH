namespace USMB_TECH.DTO
{
    public class PoleExpertisePreviewDTO
    {
        public int Id_Pole_Expertise { get; set; }
        public string Nom_Pole_Expertise { get; set; }
        public string Description { get; set; }
        public List<string> MotsCles { get; set; } = new();
    }

}
