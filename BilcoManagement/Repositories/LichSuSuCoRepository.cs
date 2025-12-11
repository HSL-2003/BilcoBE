using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public class LichSuSuCoRepository : BaseRepository<LichSuSuCo>, ILichSuSuCoRepository
    {
        public LichSuSuCoRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
