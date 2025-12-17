using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public interface INhaCungCapRepository : IRepository<NhaCungCap>
    {
    }

    public class NhaCungCapRepository : BaseRepository<NhaCungCap>, INhaCungCapRepository
    {
        public NhaCungCapRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
