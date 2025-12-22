using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class PhieuXuatKhoProfile : Profile
    {
        public PhieuXuatKhoProfile()
        {
            CreateMap<PhieuXuatKho, PhieuXuatKhoDTO>()
                .ForMember(dest => dest.TenKho, opt => opt.MapFrom(src => src.MaKhoXuatNavigation != null ? src.MaKhoXuatNavigation.TenKho : null))
                .ForMember(dest => dest.TenPhieuBaoTri, opt => opt.MapFrom(src => src.MaPhieuBaoTriNavigation != null ? $"PB-{src.MaPhieuBaoTriNavigation.MaPhieu}" : null))
                .ForMember(dest => dest.TenNguoiLapPhieu, opt => opt.MapFrom(src => src.NguoiLapPhieuNavigation != null ? src.NguoiLapPhieuNavigation.HoTen : null));
            
            CreateMap<PhieuXuatKhoDTO, PhieuXuatKho>().ReverseMap();
            CreateMap<CreatePhieuXuatKhoDTO, PhieuXuatKho>();
            CreateMap<UpdatePhieuXuatKhoDTO, PhieuXuatKho>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
