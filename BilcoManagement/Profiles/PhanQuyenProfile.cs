using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class PhanQuyenProfile : Profile
    {
        public PhanQuyenProfile()
        {
            CreateMap<PhanQuyen, PhanQuyenDTO>()
                .ForMember(dest => dest.SoLuongNguoiDung, opt => opt.Ignore());
            
            CreateMap<PhanQuyenDTO, PhanQuyen>().ReverseMap();
            CreateMap<CreatePhanQuyenDTO, PhanQuyen>();
            CreateMap<UpdatePhanQuyenDTO, PhanQuyen>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
