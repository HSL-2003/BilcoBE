using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class ChiTietDieuChuyenRepository : BaseRepository<ChiTietDieuChuyen>, IChiTietDieuChuyenRepository
    {
        public ChiTietDieuChuyenRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
