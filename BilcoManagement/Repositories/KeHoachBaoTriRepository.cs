using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class KeHoachBaoTriRepository : BaseRepository<KeHoachBaoTri>, IKeHoachBaoTriRepository
    {
        public KeHoachBaoTriRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
