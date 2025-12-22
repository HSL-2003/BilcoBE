using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public interface IPhieuNhapKhoRepository : IRepository<PhieuNhapKho>
    {
    }

    public class PhieuNhapKhoRepository : BaseRepository<PhieuNhapKho>, IPhieuNhapKhoRepository
    {
        public PhieuNhapKhoRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
