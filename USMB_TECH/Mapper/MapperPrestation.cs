using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;

namespace USMB_TECH.Mapper
{
    public class MapperPrestation : Profile
    {
        public MapperPrestation() 
        {
            CreateMap<AddPrestationDTO, Prestation>()
                .ForMember(dest => dest.Contact_USMBNavigation, opt => opt.MapFrom(src => new Contact_USMB
                {
                    Nom_Contact = src.Nom_Contact,
                }))
                .ForMember(dest => dest.Domaine_ExcellenceNavigation, opt => opt.MapFrom(src => new Domaine_Excellence 
                {
                    intitule_Domaine_Excellence = src.Nom_Domaine
                }))
                .ForMember(dest => dest.Type_PrestationNavigation, opt => opt.MapFrom(src => new Type_Prestation
                {
                    Nom_Type_Prestation = src.Type
                }))
                .ForMember(dest => dest.Unite_OeuvreNavigation, opt => opt.MapFrom(src => new Unite_Oeuvre 
                {
                    Nom_Unite_Oeuvre = src.Unite
                }))
                .ForMember(dest => dest.Precisers, opt => opt.MapFrom(src => src.Mots_Clefs.Select(mc => new Preciser
                {
                    Mot_ClefNavigation = new Mot_Clef { Nom_Mot_Clef = mc },
                    Id_Prestation = src.Id_Prestation
                }).ToList()))
                .ForMember(dest => dest.Presenters, opt => opt.MapFrom(src => src.Poles.Select(pl => new Presenter 
                {
                    Pole_ExpertiseNavigation = new Pole_Expertise { Nom_Pole_Expertise = pl},
                    Id_Prestation = src.Id_Prestation
                }).ToList()))
                .ForMember(dest => dest.LaboratoireNavigation, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Nom_Court)
                ? null 
                :new Laboratoire
                {
                    Nom_Court = src.Nom_Court
                }))
                .ForMember(dest =>dest.Photos , opt => opt.MapFrom(src => src.Photos.Select(photoDto => new Photo
                {
                    Url_Photo = photoDto.Url_Photo,
                    Nom_Photo = photoDto.Nom_Photo
                }).ToList()))
                .ForMember(dest => dest.Id_Contact, opt => opt.Ignore())
                .ForMember(dest => dest.Id_Type_Prestation, opt => opt.Ignore())
                .ForMember(dest => dest.Id_Domaine_Excellence, opt => opt.Ignore());

        }
    }
}
