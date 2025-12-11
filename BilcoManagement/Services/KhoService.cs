using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class KhoService : BaseService<Kho, KhoDTO, CreateKhoDTO, UpdateKhoDTO>, IKhoService
    {
        public KhoService(IKhoRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
