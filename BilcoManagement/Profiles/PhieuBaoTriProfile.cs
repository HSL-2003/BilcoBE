using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class PhieuBaoTriProfile : Profile
    {
        public PhieuBaoTriProfile()
        {
            CreateMap<PhieuBaoTri, PhieuBaoTriDTO>()
                .ForMember(dest => dest.TenKeHoach, opt => opt.MapFrom(src => src.MaKeHoachNavigation != null ? src.MaKeHoachNavigation.TieuDe : null))
                .ForMember(dest => dest.TenThietBi, opt => opt.MapFrom(src => src.MaThietBiNavigation != null ? src.MaThietBiNavigation.TenThietBi : null))
                .ForMember(dest => dest.TenNguoiTao, opt => opt.MapFrom(src => src.NguoiTaoNavigation != null ? src.NguoiTaoNavigation.TenDangNhap : null))
                .ForMember(dest => dest.TenNguoiDuyet, opt => opt.MapFrom(src => src.NguoiDuyetNavigation != null ? src.NguoiDuyetNavigation.TenDangNhap : null));
            
            CreateMap<PhieuBaoTriDTO, PhieuBaoTri>().ReverseMap();
            CreateMap<CreatePhieuBaoTriDTO, PhieuBaoTri>();
            CreateMap<UpdatePhieuBaoTriDTO, PhieuBaoTri>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
