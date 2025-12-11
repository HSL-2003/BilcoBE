using System.Threading.Tasks;
using BilcoManagement.Models;

namespace BilcoManagement.Interfaces
{
    public interface INguoiDungRepository : IRepository<NguoiDung>
    {
        Task<NguoiDung> GetByUserNameAsync(string userName);
        Task<bool> UserNameExistsAsync(string userName);
        Task<IEnumerable<NguoiDung>> GetPendingUsersAsync();
    }
}
