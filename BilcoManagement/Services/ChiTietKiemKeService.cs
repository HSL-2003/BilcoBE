using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class ChiTietKiemKeService : BaseService<ChiTietKiemKe, ChiTietKiemKeDTO, CreateChiTietKiemKeDTO, UpdateChiTietKiemKeDTO>, IChiTietKiemKeService
    {
        public ChiTietKiemKeService(IChiTietKiemKeRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
