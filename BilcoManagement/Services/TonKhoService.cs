using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class TonKhoService : BaseService<TonKho, TonKhoDTO, CreateTonKhoDTO, UpdateTonKhoDTO>, ITonKhoService
    {
        public TonKhoService(ITonKhoRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
