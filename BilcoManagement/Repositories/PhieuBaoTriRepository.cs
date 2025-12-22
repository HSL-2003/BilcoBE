using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public interface IPhieuBaoTriRepository : IRepository<PhieuBaoTri>
    {
    }

    public class PhieuBaoTriRepository : BaseRepository<PhieuBaoTri>, IPhieuBaoTriRepository
    {
        public PhieuBaoTriRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
