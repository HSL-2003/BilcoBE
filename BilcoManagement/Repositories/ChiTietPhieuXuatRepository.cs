using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class ChiTietPhieuXuatRepository : BaseRepository<ChiTietPhieuXuat>, IChiTietPhieuXuatRepository
    {
        public ChiTietPhieuXuatRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
