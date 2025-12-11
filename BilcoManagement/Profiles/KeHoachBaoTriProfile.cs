using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class KeHoachBaoTriProfile : Profile
    {
        public KeHoachBaoTriProfile()
        {
            CreateMap<KeHoachBaoTri, KeHoachBaoTriDTO>().ReverseMap();
            CreateMap<CreateKeHoachBaoTriDTO, KeHoachBaoTri>();
            CreateMap<UpdateKeHoachBaoTriDTO, KeHoachBaoTri>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
