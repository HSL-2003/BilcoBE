using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class DieuChuyenKhoService : BaseService<DieuChuyenKho, DieuChuyenKhoDTO, CreateDieuChuyenKhoDTO, UpdateDieuChuyenKhoDTO>, IDieuChuyenKhoService
    {
        public DieuChuyenKhoService(IDieuChuyenKhoRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
