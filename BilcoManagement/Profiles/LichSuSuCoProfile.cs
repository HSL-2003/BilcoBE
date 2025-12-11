using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class LichSuSuCoProfile : Profile
    {
        public LichSuSuCoProfile()
        {
            CreateMap<LichSuSuCo, LichSuSuCoDTO>().ReverseMap();
            CreateMap<CreateLichSuSuCoDTO, LichSuSuCo>();
            CreateMap<UpdateLichSuSuCoDTO, LichSuSuCo>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
