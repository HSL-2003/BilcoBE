using BilcoManagement.DTOs;
using BilcoManagement.Models;

namespace BilcoManagement.Interfaces
{
    public interface IPhieuBaoTriService : IService<PhieuBaoTri, PhieuBaoTriDTO, CreatePhieuBaoTriDTO, UpdatePhieuBaoTriDTO>
    {
        Task<PhieuBaoTriDTO> DuyetPhieuBaoTriAsync(int id, string ghiChu = null);
        Task<PhieuBaoTriDTO> TuChoiPhieuBaoTriAsync(int id, string lyDoTuChoi);
    }
}
