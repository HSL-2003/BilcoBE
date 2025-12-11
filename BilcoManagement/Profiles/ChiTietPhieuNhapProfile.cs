using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class ChiTietPhieuNhapProfile : Profile
    {
        public ChiTietPhieuNhapProfile()
        {
            CreateMap<ChiTietPhieuNhap, ChiTietPhieuNhapDTO>().ReverseMap();
            CreateMap<CreateChiTietPhieuNhapDTO, ChiTietPhieuNhap>();
            CreateMap<UpdateChiTietPhieuNhapDTO, ChiTietPhieuNhap>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
