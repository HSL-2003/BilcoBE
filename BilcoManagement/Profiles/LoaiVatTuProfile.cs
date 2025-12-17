using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class LoaiVatTuProfile : Profile
    {
        public LoaiVatTuProfile()
        {
            CreateMap<LoaiVatTu, LoaiVatTuDTO>().ReverseMap();
            CreateMap<CreateLoaiVatTuDTO, LoaiVatTu>();
            CreateMap<UpdateLoaiVatTuDTO, LoaiVatTu>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
