using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;
using System.Linq;
using System.Collections.Generic;

namespace USMB_TECH.Mapper
{
    public class PrestationProfile : Profile
    {
        public PrestationProfile()
        {
            CreateMap<Prestation, PrestationPreviewDTO>()
                .ForMember(dest => dest.Id_Prestation, opt => opt.MapFrom(src => src.Id_Prestation))
                .ForMember(dest => dest.Intitule_Prestation, opt => opt.MapFrom(src => src.Intitule_Prestation))
                .ForMember(dest => dest.Description_Prestation, opt => opt.MapFrom(src => src.Description_Prestation))
                .ForMember(dest => dest.MotsCles, opt => opt.MapFrom(src =>
                    src.Precisers != null
                        ? src.Precisers
                            .Where(p => p.Mot_ClefNavigation != null)
                            .Select(p => p.Mot_ClefNavigation.Nom_Mot_Clef)
                            .Distinct()
                            .ToList()
                        : new List<string>()));

            CreateMap<PrestationUpdateDto, Prestation>()
                .ForMember(dest => dest.Id_Prestation, opt => opt.MapFrom(src => src.Id_Prestation))
                .ForMember(dest => dest.Intitule_Prestation, opt => opt.MapFrom(src => src.Intitule_Prestation))
                .ForMember(dest => dest.Description_Prestation, opt => opt.MapFrom(src => src.Description_Prestation))
                .ForMember(dest => dest.Prix_Revient, opt => opt.MapFrom(src => src.Prix_Ht))
                .ForMember(dest => dest.Prix_Vente, opt => opt.MapFrom(src => src.Prix_Tva))
                .ForMember(dest => dest.Peux_Ce_Realiser_Chez_Le_Client, opt => opt.MapFrom(src => src.Peut_Realiser_Chez_Client))
                .ForMember(dest => dest.Actif, opt => opt.MapFrom(src => src.Actif))
                .ForMember(dest => dest.Id_Contact, opt => opt.MapFrom(src => src.Id_Contact))



                // FK
                .ForMember(dest => dest.Id_Unite_Oeuvre, opt => opt.MapFrom(src => src.Id_Unite_Oeuvre))
                .ForMember(dest => dest.Id_Type_Prestation, opt => opt.MapFrom(src => src.Id_Type_Prestation))
                .ForMember(dest => dest.Id_Domaine_Excellence, opt => opt.MapFrom(src => src.Id_Domaine_Excellence))
                .ForMember(dest => dest.Nom_Court, opt => opt.MapFrom(src => src.Nom_Court))


                .ForMember(dest => dest.Precisers, opt => opt.MapFrom(src => src.Precisers))
                .ForMember(dest => dest.Contact_USMBNavigation, opt => opt.Ignore());


        }
    }
}
