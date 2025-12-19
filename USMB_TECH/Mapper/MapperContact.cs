using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;


namespace USMB_TECH.Mapper
{
    public class MapperContact : Profile
    {
        public MapperContact()
        {
            CreateMap<AddContactDTO, Contact_USMB>()
            .ForMember(dest => dest.FonctionNavigation, opt => opt.MapFrom(src => new Fonction 
            {
                Nom_Fonction = src.Nom_Fonction
            }))
            .ForMember(dest => dest.Id_Fonction, opt => opt.Ignore());
            CreateMap<Contact_USMB, AddContactDTO>();
        }
    }
}
