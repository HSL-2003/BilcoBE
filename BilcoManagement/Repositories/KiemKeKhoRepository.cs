using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class KiemKeKhoRepository : BaseRepository<KiemKeKho>, IKiemKeKhoRepository
    {
        public KiemKeKhoRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
