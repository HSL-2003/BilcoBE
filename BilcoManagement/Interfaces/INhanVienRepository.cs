using BilcoManagement.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilcoManagement.Interfaces
{
    public interface INhanVienRepository
    {
        Task<IEnumerable<NhanVienDTO>> GetAllNhanViensAsync();
        Task<NhanVienDTO> GetNhanVienByIdAsync(int id);
        Task<NhanVienDTO> CreateNhanVienAsync(CreateNhanVienDTO nhanVienDto);
        Task UpdateNhanVienAsync(int id, UpdateNhanVienDTO nhanVienDto);
        Task DeleteNhanVienAsync(int id);
        Task<bool> NhanVienExists(int id);
    }
}
