using BilcoManagement.Interfaces;
using BilcoManagement.Models;
namespace BilcoManagement.Repositories
{
    public class ThietBiRepository : BaseRepository<ThietBi>, IThietBiRepository
    {
        public ThietBiRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
