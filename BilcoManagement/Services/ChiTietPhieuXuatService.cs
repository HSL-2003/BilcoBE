using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Services
{
    public class ChiTietPhieuXuatService : BaseService<ChiTietPhieuXuat, ChiTietPhieuXuatDTO, CreateChiTietPhieuXuatDTO, UpdateChiTietPhieuXuatDTO>, IChiTietPhieuXuatService
    {
        public ChiTietPhieuXuatService(IChiTietPhieuXuatRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
