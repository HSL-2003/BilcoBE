using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class HinhAnhBaoTriService : BaseService<HinhAnhBaoTri, HinhAnhBaoTriDTO, CreateHinhAnhBaoTriDTO, UpdateHinhAnhBaoTriDTO>, IHinhAnhBaoTriService
    {
        public HinhAnhBaoTriService(IHinhAnhBaoTriRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
