using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
namespace BilcoManagement.Services
{
    public class ThietBiService : BaseService<ThietBi, ThietBiDTO, CreateThietBiDTO, UpdateThietBiDTO>, IThietBiService
    {
        public ThietBiService(IThietBiRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
