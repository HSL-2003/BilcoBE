using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class TonKhoRepository : BaseRepository<TonKho>, ITonKhoRepository
    {
        public TonKhoRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
