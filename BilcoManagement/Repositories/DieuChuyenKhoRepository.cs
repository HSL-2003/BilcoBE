using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class DieuChuyenKhoRepository : BaseRepository<DieuChuyenKho>, IDieuChuyenKhoRepository
    {
        public DieuChuyenKhoRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
