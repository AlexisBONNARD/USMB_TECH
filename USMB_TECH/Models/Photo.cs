namespace USMB_TECH.Models
{
    public partial class Photo
    {
        public int Id_Photo { get; set; }

        public int Id_Equipement { get; set; }

        public int Id_Plateforme { get; set; }

        public string Nom_Photo { get; set; }

        public string Url_Photo { get; set; }
    }
}
