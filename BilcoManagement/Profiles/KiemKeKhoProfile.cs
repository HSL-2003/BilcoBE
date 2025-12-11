using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class KiemKeKhoProfile : Profile
    {
        public KiemKeKhoProfile()
        {
            CreateMap<KiemKeKho, KiemKeKhoDTO>()
                .ForMember(dest => dest.NgayKiemKe, opt => opt.MapFrom(src => src.NgayKiemKe.ToDateTime(TimeOnly.MinValue)))
                .ReverseMap()
                .ForMember(dest => dest.NgayKiemKe, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.NgayKiemKe)));
            
            CreateMap<CreateKiemKeKhoDTO, KiemKeKho>()
                .ForMember(dest => dest.NgayKiemKe, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.NgayKiemKe)));
            
            CreateMap<UpdateKiemKeKhoDTO, KiemKeKho>()
                .ForMember(dest => dest.NgayKiemKe, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.NgayKiemKe)))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
