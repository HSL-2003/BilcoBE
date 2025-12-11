using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class ChiTietPhieuXuatProfile : Profile
    {
        public ChiTietPhieuXuatProfile()
        {
            CreateMap<ChiTietPhieuXuat, ChiTietPhieuXuatDTO>().ReverseMap();
            CreateMap<CreateChiTietPhieuXuatDTO, ChiTietPhieuXuat>();
            CreateMap<UpdateChiTietPhieuXuatDTO, ChiTietPhieuXuat>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
