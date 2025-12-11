using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class ChiTietDieuChuyenProfile : Profile
    {
        public ChiTietDieuChuyenProfile()
        {
            CreateMap<ChiTietDieuChuyen, ChiTietDieuChuyenDTO>().ReverseMap();
            CreateMap<CreateChiTietDieuChuyenDTO, ChiTietDieuChuyen>();
            CreateMap<UpdateChiTietDieuChuyenDTO, ChiTietDieuChuyen>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
