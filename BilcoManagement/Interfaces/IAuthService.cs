using System.Collections.Generic;
using System.Threading.Tasks;
using BilcoManagement.DTOs;

namespace BilcoManagement.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterNguoiDungDTO registerDto);
        Task<AuthResponseDTO> LoginAsync(LoginRequestDTO loginDto);
        Task<NguoiDungDTO> ApproveUserAsync(int userId, ApproveNguoiDungDTO approveDto);
        Task<IEnumerable<NguoiDungDTO>> GetPendingUsersAsync();
        Task<NguoiDungDTO> AdminCreateUserAsync(AdminCreateNguoiDungDTO createDto);
        Task<IEnumerable<NguoiDungDTO>> GetAllUsersAsync();
        Task<bool> UpdateNhanVienInfoAsync(int maNV, UpdateNhanVienDto updateDto);
        Task<NguoiDungDTO> UpdateUserProfileAsync(int userId, UpdateUserProfileDto updateDto);
        Task<NguoiDungDTO> GetUserByIDAsync(int userId);
        Task<NguoiDungDTO> DeleteUserByIdAsync(int userId);
    }
}
