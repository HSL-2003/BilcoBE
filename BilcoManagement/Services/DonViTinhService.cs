using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class DonViTinhService : BaseService<DonViTinh, DonViTinhDTO, CreateDonViTinhDTO, UpdateDonViTinhDTO>, IDonViTinhService
    {
        public DonViTinhService(IDonViTinhRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
