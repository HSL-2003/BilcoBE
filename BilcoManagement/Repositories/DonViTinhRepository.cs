using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class DonViTinhRepository : BaseRepository<DonViTinh>, IDonViTinhRepository
    {
        public DonViTinhRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
