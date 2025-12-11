using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class ChiTietPhuTungProfile : Profile
    {
        public ChiTietPhuTungProfile()
        {
            CreateMap<ChiTietPhuTung, ChiTietPhuTungDTO>().ReverseMap();
            CreateMap<CreateChiTietPhuTungDTO, ChiTietPhuTung>();
            CreateMap<UpdateChiTietPhuTungDTO, ChiTietPhuTung>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
