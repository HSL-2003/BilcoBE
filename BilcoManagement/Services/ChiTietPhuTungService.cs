using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class ChiTietPhuTungService : BaseService<ChiTietPhuTung, ChiTietPhuTungDTO, CreateChiTietPhuTungDTO, UpdateChiTietPhuTungDTO>, IChiTietPhuTungService
    {
        public ChiTietPhuTungService(IChiTietPhuTungRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
