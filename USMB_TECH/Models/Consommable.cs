namespace USMB_TECH.Models
{
    public partial class Consommable
    {
        public int Id_Consommable {  get; set; }

        public int Id_Unite { get; set; }

        public string Nom_Consommable { get; set; } 

        public double Prix_Unite { get; set; }

        public double Prix_Forfait { get; set; }

        public int Forfait { get; set; }
    }
}
