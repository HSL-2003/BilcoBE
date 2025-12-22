using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public interface IPhieuXuatKhoRepository : IRepository<PhieuXuatKho>
    {
    }

    public class PhieuXuatKhoRepository : BaseRepository<PhieuXuatKho>, IPhieuXuatKhoRepository
    {
        public PhieuXuatKhoRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
