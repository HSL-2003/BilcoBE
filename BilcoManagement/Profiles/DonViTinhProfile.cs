using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Profiles
{
    public class DonViTinhProfile : Profile
    {
        public DonViTinhProfile()
        {
            CreateMap<DonViTinh, DonViTinhDTO>().ReverseMap();
            CreateMap<CreateDonViTinhDTO, DonViTinh>();
            CreateMap<UpdateDonViTinhDTO, DonViTinh>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
