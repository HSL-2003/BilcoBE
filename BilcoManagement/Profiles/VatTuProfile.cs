using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class VatTuProfile : Profile
    {
        public VatTuProfile()
        {
            CreateMap<VatTu, VatTuDTO>()
                .ForMember(dest => dest.TenLoaiVT, opt => opt.MapFrom(src => src.MaLoaiVTNavigation != null ? src.MaLoaiVTNavigation.TenLoaiVT : null))
                .ForMember(dest => dest.TenDVT, opt => opt.MapFrom(src => src.MaDVTNavigation != null ? src.MaDVTNavigation.TenDVT : null))
                .ForMember(dest => dest.TenNCC, opt => opt.MapFrom(src => src.MaNCCNavigation != null ? src.MaNCCNavigation.TenNCC : null))
                .ForMember(dest => dest.TenNguoiTao, opt => opt.MapFrom(src => src.NguoiTaoNavigation != null ? src.NguoiTaoNavigation.TenDangNhap : null))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => 
                {
                    // Debug: Log mapping attempt
                    System.Diagnostics.Debug.WriteLine($"Mapping {srcMember?.GetType().Name} to {dest?.GetType().Name}");
                    return srcMember != null;
                }));
            
            CreateMap<VatTuDTO, VatTu>().ReverseMap();
            CreateMap<CreateVatTuDTO, VatTu>();
            CreateMap<UpdateVatTuDTO, VatTu>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
