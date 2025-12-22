using BilcoManagement.Interfaces;
using BilcoManagement.Models;

namespace BilcoManagement.Repositories
{
    public interface IPhanQuyenRepository : IRepository<PhanQuyen>
    {
    }

    public class PhanQuyenRepository : BaseRepository<PhanQuyen>, IPhanQuyenRepository
    {
        public PhanQuyenRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }
    }
}
