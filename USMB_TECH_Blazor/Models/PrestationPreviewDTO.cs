namespace USMB_TECH_Blazor.Models
{
    public class PrestationPreviewDTO
    {
        public int Id_Prestation { get; set; }
        public string Intitule_Prestation { get; set; }
        public string Description_Prestation { get; set; }
        public List<string> MotsCles { get; set; } = new();
    }

}
