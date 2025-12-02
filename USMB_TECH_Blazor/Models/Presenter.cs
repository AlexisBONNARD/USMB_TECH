using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Presenter
    {
        public int Id_Pole_Expertise { get; set; }

        public int Id_Prestation { get; set; }

        public virtual Pole_Expertise? Pole_Expertise { get; set; }
        public virtual Prestation? prestationNavigation { get; set; }
    }
}
