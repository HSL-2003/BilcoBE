using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class HinhAnhBaoTriProfile : Profile
    {
        public HinhAnhBaoTriProfile()
        {
            CreateMap<HinhAnhBaoTri, HinhAnhBaoTriDTO>().ReverseMap();
            CreateMap<CreateHinhAnhBaoTriDTO, HinhAnhBaoTri>();
            CreateMap<UpdateHinhAnhBaoTriDTO, HinhAnhBaoTri>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
