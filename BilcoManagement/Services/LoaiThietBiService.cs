using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class LoaiThietBiService : BaseService<LoaiThietBi, LoaiThietBiDTO, CreateLoaiThietBiDTO, UpdateLoaiThietBiDTO>, ILoaiThietBiService
    {
        public LoaiThietBiService(ILoaiThietBiRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
