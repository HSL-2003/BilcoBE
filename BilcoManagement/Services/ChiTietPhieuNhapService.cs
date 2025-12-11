using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class ChiTietPhieuNhapService : BaseService<ChiTietPhieuNhap, ChiTietPhieuNhapDTO, CreateChiTietPhieuNhapDTO, UpdateChiTietPhieuNhapDTO>, IChiTietPhieuNhapService
    {
        public ChiTietPhieuNhapService(IChiTietPhieuNhapRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
