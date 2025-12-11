using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class TonKhoProfile : Profile
    {
        public TonKhoProfile()
        {
            CreateMap<TonKho, TonKhoDTO>().ReverseMap();
            CreateMap<CreateTonKhoDTO, TonKho>();
            CreateMap<UpdateTonKhoDTO, TonKho>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
