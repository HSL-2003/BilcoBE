using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class KeHoachBaoTriService : BaseService<KeHoachBaoTri, KeHoachBaoTriDTO, CreateKeHoachBaoTriDTO, UpdateKeHoachBaoTriDTO>, IKeHoachBaoTriService
    {
        public KeHoachBaoTriService(IKeHoachBaoTriRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
