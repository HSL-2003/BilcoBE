using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public interface IVatTuRepository : IRepository<VatTu>
    {
    }

    public class VatTuRepository : BaseRepository<VatTu>, IVatTuRepository
    {
        public VatTuRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
