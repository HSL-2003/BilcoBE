using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class ChiTietKiemKeRepository : BaseRepository<ChiTietKiemKe>, IChiTietKiemKeRepository
    {
        public ChiTietKiemKeRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
