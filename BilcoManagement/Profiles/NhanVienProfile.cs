using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class NhanVienProfile : Profile
    {
        public NhanVienProfile()
        {
            CreateMap<Models.NhanVien, NhanVienDTO>().ReverseMap();
            CreateMap<CreateNhanVienDTO, Models.NhanVien>();
            CreateMap<UpdateNhanVienDTO, Models.NhanVien>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                
            // Add mapping for UpdateNhanVienDto
            CreateMap<UpdateNhanVienDto, Models.NhanVien>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
