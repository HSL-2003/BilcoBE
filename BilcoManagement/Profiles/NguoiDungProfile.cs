using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class NguoiDungProfile : Profile
    {
        public NguoiDungProfile()
        {
            CreateMap<NguoiDung, NguoiDungDTO>().ReverseMap();
            CreateMap<CreateNguoiDungDTO, NguoiDung>();
            CreateMap<UpdateNguoiDungDTO, NguoiDung>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<RegisterNguoiDungDTO, NguoiDung>();
            CreateMap<AdminCreateNguoiDungDTO, NguoiDung>()
               .ForMember(dest => dest.MaNV, opt => opt.Ignore()) // Bỏ qua MaNV vì sẽ được xử lý riêng
               .ForMember(dest => dest.MatKhau, opt => opt.Ignore()) // Bỏ qua MatKhau vì sẽ được hash
               .ForMember(dest => dest.NgayTao, opt => opt.Ignore()) // Sẽ được gán trong service
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Sẽ được gán trong service
               .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Sẽ được gán trong service
            
            // Thêm cấu hình cho UpdateNhanVienDto -> NhanVien
            CreateMap<UpdateNhanVienDto, NhanVien>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
