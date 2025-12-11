using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class KhoRepository : BaseRepository<Kho>, IKhoRepository
    {
        public KhoRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
