using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class ChiTietDieuChuyenService : BaseService<ChiTietDieuChuyen, ChiTietDieuChuyenDTO, CreateChiTietDieuChuyenDTO, UpdateChiTietDieuChuyenDTO>, IChiTietDieuChuyenService
    {
        public ChiTietDieuChuyenService(IChiTietDieuChuyenRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
