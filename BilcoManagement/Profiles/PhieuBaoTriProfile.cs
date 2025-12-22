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
                .ForMember(dest => dest.TenNhanVienThucHien, opt => opt.MapFrom(src => src.NhanVienThucHienNavigation != null ? src.NhanVienThucHienNavigation.HoTen : null))
                .ForMember(dest => dest.TenNguoiXacNhan, opt => opt.MapFrom(src => src.NguoiXacNhanNavigation != null ? src.NguoiXacNhanNavigation.HoTen : null));
            
            CreateMap<PhieuBaoTriDTO, PhieuBaoTri>().ReverseMap();
            CreateMap<CreatePhieuBaoTriDTO, PhieuBaoTri>();
            CreateMap<UpdatePhieuBaoTriDTO, PhieuBaoTri>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
