using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class HinhAnhBaoTriRepository : BaseRepository<HinhAnhBaoTri>, IHinhAnhBaoTriRepository
    {
        public HinhAnhBaoTriRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
