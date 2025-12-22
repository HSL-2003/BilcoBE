using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class NhanVienProfile : Profile
    {
        public NhanVienProfile()
        {
            CreateMap<Models.NhanVien, NhanVienDTO>()
                .ForMember(dest => dest.TenNguoiDung, opt => opt.MapFrom(src => src.User != null ? src.User.TenDangNhap : null));
            
            CreateMap<NhanVienDTO, Models.NhanVien>().ReverseMap();
            CreateMap<CreateNhanVienDTO, Models.NhanVien>();
            CreateMap<UpdateNhanVienDTO, Models.NhanVien>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
