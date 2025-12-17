using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
using BilcoManagement.Repositories;

namespace BilcoManagement.Services
{
    public class NhaCungCapService : BaseService<NhaCungCap, NhaCungCapDTO, CreateNhaCungCapDTO, UpdateNhaCungCapDTO>, INhaCungCapService
    {
        public NhaCungCapService(INhaCungCapRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
