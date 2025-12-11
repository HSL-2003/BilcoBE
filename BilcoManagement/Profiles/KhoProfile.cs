using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class KhoProfile : Profile
    {
        public KhoProfile()
        {
            CreateMap<Kho, KhoDTO>().ReverseMap();
            CreateMap<CreateKhoDTO, Kho>();
            CreateMap<UpdateKhoDTO, Kho>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
