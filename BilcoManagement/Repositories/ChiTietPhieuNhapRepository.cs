using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class ChiTietPhieuNhapRepository : BaseRepository<ChiTietPhieuNhap>, IChiTietPhieuNhapRepository
    {
        public ChiTietPhieuNhapRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
