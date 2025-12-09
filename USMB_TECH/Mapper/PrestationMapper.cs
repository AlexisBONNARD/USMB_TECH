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
        }
    }
}
