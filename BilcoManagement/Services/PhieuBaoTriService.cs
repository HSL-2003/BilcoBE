using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
using Microsoft.EntityFrameworkCore;
using BilcoManagement.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BilcoManagement.Services
{
    public class PhieuBaoTriService : BaseService<PhieuBaoTri, PhieuBaoTriDTO, CreatePhieuBaoTriDTO, UpdatePhieuBaoTriDTO>, IPhieuBaoTriService
    {
        private readonly QuanLyChatLuongSanPhamContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhieuBaoTriService(IPhieuBaoTriRepository repository, IMapper mapper, QuanLyChatLuongSanPhamContext context, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public override async Task<PhieuBaoTriDTO> CreateAsync(CreatePhieuBaoTriDTO createDto)
        {
            // Lấy thông tin user hiện tại
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                throw new UnauthorizedAccessException("Không xác định được người dùng hiện tại");
            }

            // Kiểm tra kế hoạch bảo trì có tồn tại không
            if (createDto.MaKeHoach.HasValue)
            {
                var keHoachExists = await _context.KeHoachBaoTris
                    .AnyAsync(kh => kh.MaKeHoach == createDto.MaKeHoach.Value);
                    
                if (!keHoachExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy kế hoạch bảo trì với mã: {createDto.MaKeHoach}");
                }
            }

            // Kiểm tra thiết bị có tồn tại không
            if (createDto.MaThietBi.HasValue)
            {
                var thietBiExists = await _context.ThietBis
                    .AnyAsync(tb => tb.MaThietBi == createDto.MaThietBi.Value);
                    
                if (!thietBiExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy thiết bị với mã: {createDto.MaThietBi}");
                }
            }

            // Kiểm tra nhân viên thực hiện có tồn tại không
            if (createDto.NhanVienThucHien.HasValue)
            {
                var nhanVienExists = await _context.NhanViens
                    .AnyAsync(nv => nv.MaNV == createDto.NhanVienThucHien.Value);
                    
                if (!nhanVienExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy nhân viên với mã: {createDto.NhanVienThucHien}");
                }
            }

            // Tạo PhieuBaoTri entity
            var phieuBaoTri = _mapper.Map<PhieuBaoTri>(createDto);
            phieuBaoTri.ThoiGianBatDau = phieuBaoTri.ThoiGianBatDau ?? DateTime.Now;
            phieuBaoTri.TrangThai = "ChoDuyet"; // Mặc định trạng thái chờ duyệt
            phieuBaoTri.NguoiTao = currentUserId; // Gán người tạo là user hiện tại
            phieuBaoTri.TrangThaiDuyet = "ChoDuyet"; // Mặc định chờ duyệt

            await _repository.AddAsync(phieuBaoTri);
            await _context.SaveChangesAsync();

            // Load navigation properties for DTO mapping
            await LoadNavigationProperties(phieuBaoTri);

            // Manual mapping
            var dto = MapToDTO(phieuBaoTri);
            return dto;
        }

        public override async Task<IEnumerable<PhieuBaoTriDTO>> GetAllAsync()
        {
            var currentUserId = GetCurrentUserId();
            var currentUserRole = GetCurrentUserRole();
            
            IQueryable<PhieuBaoTri> query = _context.PhieuBaoTris;
            
            // Nếu không phải admin, chỉ lấy các phiếu do người dùng hiện tại tạo
            if (currentUserRole != "Admin" && currentUserId.HasValue)
            {
                query = query.Where(pb => pb.NguoiTao == currentUserId);
            }
            
            var entities = await query
                .Include(pb => pb.MaKeHoachNavigation)
                .Include(pb => pb.MaThietBiNavigation)
                .Include(pb => pb.NguoiTaoNavigation)
                .Include(pb => pb.NguoiDuyetNavigation)
                .ToListAsync();
            
            // Manual mapping for each entity
            var dtos = entities.Select(MapToDTO);
            return dtos;
        }

        public override async Task<PhieuBaoTriDTO> GetByIdAsync(int id)
        {
            var entity = await _context.PhieuBaoTris
                .Include(pb => pb.MaKeHoachNavigation)
                .Include(pb => pb.MaThietBiNavigation)
                .Include(pb => pb.NguoiTaoNavigation)
                .Include(pb => pb.NguoiDuyetNavigation)
                .FirstOrDefaultAsync(pb => pb.MaPhieu == id);
            
            if (entity == null) return null;
            
            return MapToDTO(entity);
        }

        public override async Task UpdateAsync(int id, UpdatePhieuBaoTriDTO updateDto)
        {
            // Kiểm tra quyền - chỉ admin mới được sửa
            var currentUserRole = GetCurrentUserRole();
            if (currentUserRole != "Admin")
            {
                throw new UnauthorizedAccessException("Chỉ admin mới được sửa phiếu bảo trì");
            }
            
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Phiếu bảo trì với ID {id} không tồn tại");
            }

            // Kiểm tra các quan hệ nếu có thay đổi
            await ValidateRelationships(updateDto, id);

            // Nếu có người xác nhận, gán ngày xác nhận
            // NgayXacNhan field không còn tồn tại trong model mới
            // Có thể thêm logic xử lý khác nếu cần

            _mapper.Map(updateDto, entity);
            await _repository.UpdateAsync(id, entity);
        }

        private async Task LoadNavigationProperties(PhieuBaoTri phieuBaoTri)
        {
            await _context.Entry(phieuBaoTri).Reference(pb => pb.MaKeHoachNavigation).LoadAsync();
            await _context.Entry(phieuBaoTri).Reference(pb => pb.MaThietBiNavigation).LoadAsync();
            await _context.Entry(phieuBaoTri).Reference(pb => pb.NguoiTaoNavigation).LoadAsync();
            await _context.Entry(phieuBaoTri).Reference(pb => pb.NguoiDuyetNavigation).LoadAsync();
        }

        private PhieuBaoTriDTO MapToDTO(PhieuBaoTri entity)
        {
            return new PhieuBaoTriDTO
            {
                MaPhieu = entity.MaPhieu,
                MaKeHoach = entity.MaKeHoach,
                MaThietBi = entity.MaThietBi,
                NguoiTao = entity.NguoiTao,
                NguoiDuyet = entity.NguoiDuyet,
                TrangThaiDuyet = entity.TrangThaiDuyet,
                LyDoTuChoi = entity.LyDoTuChoi,
                NgayDuyet = entity.NgayDuyet,
                ThoiGianBatDau = entity.ThoiGianBatDau,
                ThoiGianKetThuc = entity.ThoiGianKetThuc,
                TinhTrangTruocBT = entity.TinhTrangTruocBT,
                TinhTrangSauBT = entity.TinhTrangSauBT,
                KetQua = entity.KetQua,
                GhiChu = entity.GhiChu,
                TrangThai = entity.TrangThai,
                TenKeHoach = entity.MaKeHoachNavigation?.TieuDe,
                TenThietBi = entity.MaThietBiNavigation?.TenThietBi,
                TenNguoiTao = entity.NguoiTaoNavigation?.TenDangNhap,
                TenNguoiDuyet = entity.NguoiDuyetNavigation?.TenDangNhap,
            };
        }

        public override async Task DeleteAsync(int id)
        {
            // Kiểm tra quyền - chỉ admin mới được xóa
            var currentUserRole = GetCurrentUserRole();
            if (currentUserRole != "Admin")
            {
                throw new UnauthorizedAccessException("Chỉ admin mới được xóa phiếu bảo trì");
            }
            
            await base.DeleteAsync(id);
        }
        
        private int? GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }
        
        private string GetCurrentUserRole()
        {
            var roleClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role);
            return roleClaim?.Value ?? string.Empty;
        }

        public async Task<PhieuBaoTriDTO> DuyetPhieuBaoTriAsync(int id, string ghiChu = null)
        {
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                throw new UnauthorizedAccessException("Không xác định được người duyệt");
            }

            var phieuBaoTri = await _repository.GetByIdAsync(id);
            if (phieuBaoTri == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy phiếu bảo trì với ID: {id}");
            }

            // Cập nhật thông tin duyệt
            phieuBaoTri.TrangThaiDuyet = "DaDuyet";
            phieuBaoTri.NguoiDuyet = currentUserId;
            phieuBaoTri.NgayDuyet = DateTime.Now;
            phieuBaoTri.GhiChu = ghiChu ?? phieuBaoTri.GhiChu;

            await _repository.UpdateAsync(phieuBaoTri.MaPhieu, phieuBaoTri);
            await _context.SaveChangesAsync();

            await LoadNavigationProperties(phieuBaoTri);
            return MapToDTO(phieuBaoTri);
        }

        public async Task<PhieuBaoTriDTO> TuChoiPhieuBaoTriAsync(int id, string lyDoTuChoi)
        {
            if (string.IsNullOrWhiteSpace(lyDoTuChoi))
            {
                throw new ArgumentException("Lý do từ chối không được để trống");
            }

            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                throw new UnauthorizedAccessException("Không xác định được người từ chối");
            }

            var phieuBaoTri = await _repository.GetByIdAsync(id);
            if (phieuBaoTri == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy phiếu bảo trì với ID: {id}");
            }

            // Cập nhật thông tin từ chối
            phieuBaoTri.TrangThaiDuyet = "TuChoi";
            phieuBaoTri.LyDoTuChoi = lyDoTuChoi;
            phieuBaoTri.NguoiDuyet = currentUserId;
            phieuBaoTri.NgayDuyet = DateTime.Now;

            await _repository.UpdateAsync(phieuBaoTri.MaPhieu, phieuBaoTri);
            await _context.SaveChangesAsync();

            await LoadNavigationProperties(phieuBaoTri);
            return MapToDTO(phieuBaoTri);
        }

        private async Task ValidateRelationships(UpdatePhieuBaoTriDTO updateDto, int currentId)
        {
            if (updateDto.MaKeHoach.HasValue)
            {
                var keHoachExists = await _context.KeHoachBaoTris
                    .AnyAsync(kh => kh.MaKeHoach == updateDto.MaKeHoach.Value);
                    
                if (!keHoachExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy kế hoạch bảo trì với mã: {updateDto.MaKeHoach}");
                }
            }

            if (updateDto.MaThietBi.HasValue)
            {
                var thietBiExists = await _context.ThietBis
                    .AnyAsync(tb => tb.MaThietBi == updateDto.MaThietBi.Value);
                    
                if (!thietBiExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy thiết bị với mã: {updateDto.MaThietBi}");
                }
            }

            if (updateDto.NhanVienThucHien.HasValue)
            {
                var nhanVienExists = await _context.NhanViens
                    .AnyAsync(nv => nv.MaNV == updateDto.NhanVienThucHien.Value);
                    
                if (!nhanVienExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy nhân viên với mã: {updateDto.NhanVienThucHien}");
                }
            }
        }
    }
}
