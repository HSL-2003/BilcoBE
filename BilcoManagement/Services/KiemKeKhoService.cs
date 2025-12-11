using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class KiemKeKhoService : BaseService<KiemKeKho, KiemKeKhoDTO, CreateKiemKeKhoDTO, UpdateKiemKeKhoDTO>, IKiemKeKhoService
    {
        public KiemKeKhoService(IKiemKeKhoRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
