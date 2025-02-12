using AutoMapper;
using Hiring_Date_API.DTOs;
using Hiring_Date_API.Models;

namespace Hiring_Date_API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {//for GET
            CreateMap<Hiring, HiringDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src =>
                    src.Company_CompanyId != null ? src.Company_CompanyId.CompanyName : "Unknown"));

            // Mapping for POST (Create) requests
            CreateMap<HiringCreateDto, Hiring>();

            // Mapping for PUT (Update) requests
            CreateMap<HiringUpdateDto, Hiring>();
        }
    }
}