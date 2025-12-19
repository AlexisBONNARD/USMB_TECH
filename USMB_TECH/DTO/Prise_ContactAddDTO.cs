using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class Prise_ContactAddDTO
    {
        public string Nom_Contact { get; set; } = string.Empty;
        public string Prenom_Contact { get; set; } = string.Empty;
        public string Email_Contact { get; set; } = string.Empty;
        public string Entreprise_Contact { get; set; } = string.Empty;
        public string Description_besoins { get; set; } = string.Empty;

        // Paramétrage interne côté serveur
        public int Id_Type_Client { get; set; } = 1; // Exemple : "Particulier" ou "Externe"
    }
}
