using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class DieuChuyenKhoProfile : Profile
    {
        public DieuChuyenKhoProfile()
        {
            CreateMap<DieuChuyenKho, DieuChuyenKhoDTO>().ReverseMap();
            CreateMap<CreateDieuChuyenKhoDTO, DieuChuyenKho>();
            CreateMap<UpdateDieuChuyenKhoDTO, DieuChuyenKho>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
