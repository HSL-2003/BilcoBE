using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public interface ILoaiVatTuRepository : IRepository<LoaiVatTu>
    {
    }

    public class LoaiVatTuRepository : BaseRepository<LoaiVatTu>, ILoaiVatTuRepository
    {
        public LoaiVatTuRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
