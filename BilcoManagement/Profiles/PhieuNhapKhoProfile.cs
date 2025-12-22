using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class PhieuNhapKhoProfile : Profile
    {
        public PhieuNhapKhoProfile()
        {
            CreateMap<PhieuNhapKho, PhieuNhapKhoDTO>()
                .ForMember(dest => dest.TenKho, opt => opt.MapFrom(src => src.MaKhoNhapNavigation != null ? src.MaKhoNhapNavigation.TenKho : null))
                .ForMember(dest => dest.TenNCC, opt => opt.MapFrom(src => src.MaNCCNavigation != null ? src.MaNCCNavigation.TenNCC : null))
                .ForMember(dest => dest.TenNguoiLapPhieu, opt => opt.MapFrom(src => src.NguoiLapPhieuNavigation != null ? src.NguoiLapPhieuNavigation.HoTen : null));
            
            CreateMap<PhieuNhapKhoDTO, PhieuNhapKho>().ReverseMap();
            CreateMap<CreatePhieuNhapKhoDTO, PhieuNhapKho>();
            CreateMap<UpdatePhieuNhapKhoDTO, PhieuNhapKho>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
