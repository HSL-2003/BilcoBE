using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class LoaiThietBiRepository : BaseRepository<LoaiThietBi>, ILoaiThietBiRepository
    {
        public LoaiThietBiRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
