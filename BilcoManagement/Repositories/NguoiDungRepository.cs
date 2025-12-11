using System.Threading.Tasks;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BilcoManagement.Repositories
{
    public class NguoiDungRepository : BaseRepository<NguoiDung>, INguoiDungRepository
    {
        public NguoiDungRepository(QuanLyChatLuongSanPhamContext context) : base(context)
        {
        }

        public async Task<NguoiDung> GetByUserNameAsync(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.TenDangNhap == userName);
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _dbSet.AnyAsync(u => u.TenDangNhap == userName);
        }
        public async Task<IEnumerable<NguoiDung>> GetPendingUsersAsync()
        {
            return await _dbSet
                .Where(u => u.TrangThai == false || u.IsActive == false)
                .ToListAsync();
        }
    }
}
