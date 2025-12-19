using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class KeHoachBaoTriProfile : Profile
    {
        public KeHoachBaoTriProfile()
        {
            CreateMap<KeHoachBaoTri, KeHoachBaoTriDTO>()
                .ForMember(dest => dest.TenNguoiTao, opt => opt.MapFrom(src => src.NguoiTaoNavigation != null ? src.NguoiTaoNavigation.TenDangNhap : null))
                .ForMember(dest => dest.TenThietBi, opt => opt.MapFrom(src => src.MaThietBiNavigation != null ? src.MaThietBiNavigation.TenThietBi : null));
            
            CreateMap<KeHoachBaoTriDTO, KeHoachBaoTri>().ReverseMap();
            CreateMap<CreateKeHoachBaoTriDTO, KeHoachBaoTri>();
            CreateMap<UpdateKeHoachBaoTriDTO, KeHoachBaoTri>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
