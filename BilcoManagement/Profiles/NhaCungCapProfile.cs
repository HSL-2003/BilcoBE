using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class NhaCungCapProfile : Profile
    {
        public NhaCungCapProfile()
        {
            CreateMap<NhaCungCap, NhaCungCapDTO>().ReverseMap();
            CreateMap<CreateNhaCungCapDTO, NhaCungCap>();
            CreateMap<UpdateNhaCungCapDTO, NhaCungCap>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
