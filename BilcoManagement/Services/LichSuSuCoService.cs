using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class LichSuSuCoService : BaseService<LichSuSuCo, LichSuSuCoDTO, CreateLichSuSuCoDTO, UpdateLichSuSuCoDTO>, ILichSuSuCoService
    {
        public LichSuSuCoService(ILichSuSuCoRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
