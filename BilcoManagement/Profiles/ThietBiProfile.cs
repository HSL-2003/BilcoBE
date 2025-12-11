using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;
namespace BilcoManagement.Profiles
{
    public class ThietBiProfile : Profile
    {
        public ThietBiProfile()
        {
            CreateMap<ThietBi, ThietBiDTO>().ReverseMap();
            CreateMap<CreateThietBiDTO, ThietBi>();
            CreateMap<UpdateThietBiDTO, ThietBi>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
