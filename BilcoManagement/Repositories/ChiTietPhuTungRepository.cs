using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class ChiTietPhuTungRepository : BaseRepository<ChiTietPhuTung>, IChiTietPhuTungRepository
    {
        public ChiTietPhuTungRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
