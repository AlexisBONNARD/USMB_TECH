using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using USMB_TECH.DTO;
using USMB_TECH.Models;
using USMB_TECH_Blazor.Models;

namespace USMB_TECH.Mapper
{
    public class DomaineExcellenceProfile : Profile
    {
        public DomaineExcellenceProfile()
        {
            CreateMap<Domaine_Excellence, DomaineExcellenceDTO>()
                .ForMember(dest => dest.Id_Domaine_Excellence, opt => opt.MapFrom(src => src.Id_Domaine_Excellence))
                .ForMember(dest => dest.Intitule_Domaine_Excellence, opt => opt.MapFrom(src => src.intitule_Domaine_Excellence))
                .ForMember(dest => dest.Description_Domaine_Excellence, opt => opt.MapFrom(src => src.Description_Domaine_Excellence));
        }
    }
}
