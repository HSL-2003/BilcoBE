using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class LoaiThietBiProfile : Profile
    {
        public LoaiThietBiProfile()
        {
            CreateMap<LoaiThietBi, LoaiThietBiDTO>().ReverseMap();
            CreateMap<CreateLoaiThietBiDTO, LoaiThietBi>();
            CreateMap<UpdateLoaiThietBiDTO, LoaiThietBi>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
