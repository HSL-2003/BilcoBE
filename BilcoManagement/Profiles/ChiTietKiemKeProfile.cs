using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class ChiTietKiemKeProfile : Profile
    {
        public ChiTietKiemKeProfile()
        {
            CreateMap<ChiTietKiemKe, ChiTietKiemKeDTO>().ReverseMap();
            CreateMap<CreateChiTietKiemKeDTO, ChiTietKiemKe>();
            CreateMap<UpdateChiTietKiemKeDTO, ChiTietKiemKe>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
