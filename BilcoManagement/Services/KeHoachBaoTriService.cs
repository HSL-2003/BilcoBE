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
    public class KeHoachBaoTriService : BaseService<KeHoachBaoTri, KeHoachBaoTriDTO, CreateKeHoachBaoTriDTO, UpdateKeHoachBaoTriDTO>, IKeHoachBaoTriService
    {
        private readonly QuanLyChatLuongSanPhamContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public KeHoachBaoTriService(IKeHoachBaoTriRepository repository, IMapper mapper, QuanLyChatLuongSanPhamContext context, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public override async Task<KeHoachBaoTriDTO> CreateAsync(CreateKeHoachBaoTriDTO createDto)
        {
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

            // Lấy ID người dùng hiện tại từ token
            var currentUserId = GetCurrentUserId();
            
            // Tạo KeHoachBaoTri entity và gán người tạo
            var keHoachBaoTri = _mapper.Map<KeHoachBaoTri>(createDto);
            keHoachBaoTri.NguoiTao = currentUserId;
            keHoachBaoTri.NgayTao = DateTime.Now;

            await _repository.AddAsync(keHoachBaoTri);
            await _context.SaveChangesAsync();

            // Load navigation properties for DTO mapping
            await _context.Entry(keHoachBaoTri).Reference(k => k.MaThietBiNavigation).LoadAsync();
            await _context.Entry(keHoachBaoTri).Reference(k => k.NguoiTaoNavigation).LoadAsync();

            // Debug logging
            System.Diagnostics.Debug.WriteLine($"Created - MaThietBi: {keHoachBaoTri.MaThietBi}, TenThietBi: {keHoachBaoTri.MaThietBiNavigation?.TenThietBi}");
            System.Diagnostics.Debug.WriteLine($"Created - NguoiTao: {keHoachBaoTri.NguoiTao}, TenNguoiTao: {keHoachBaoTri.NguoiTaoNavigation?.TenDangNhap}");

            // Try manual mapping as fallback
            var dto = new KeHoachBaoTriDTO
            {
                MaKeHoach = keHoachBaoTri.MaKeHoach,
                TieuDe = keHoachBaoTri.TieuDe,
                MaThietBi = keHoachBaoTri.MaThietBi,
                LoaiBaoTri = keHoachBaoTri.LoaiBaoTri,
                ChuKyBaoTri = keHoachBaoTri.ChuKyBaoTri,
                NgayBatDau = keHoachBaoTri.NgayBatDau,
                NgayKetThuc = keHoachBaoTri.NgayKetThuc,
                MoTa = keHoachBaoTri.MoTa,
                TrangThai = keHoachBaoTri.TrangThai,
                NguoiTao = keHoachBaoTri.NguoiTao,
                NgayTao = keHoachBaoTri.NgayTao,
                TenThietBi = keHoachBaoTri.MaThietBiNavigation?.TenThietBi,
                TenNguoiTao = keHoachBaoTri.NguoiTaoNavigation?.TenDangNhap
            };
            
            // Debug after manual mapping
            System.Diagnostics.Debug.WriteLine($"After manual mapping - TenThietBi: {dto.TenThietBi}");
            System.Diagnostics.Debug.WriteLine($"After manual mapping - TenNguoiTao: {dto.TenNguoiTao}");

            return dto;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            
            // Fallback: try with "userId" claim name
            var fallbackClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("userId");
            if (fallbackClaim != null && int.TryParse(fallbackClaim.Value, out var fallbackUserId))
            {
                return fallbackUserId;
            }
            
            throw new UnauthorizedAccessException("Không thể xác định người dùng hiện tại");
        }

        public override async Task<IEnumerable<KeHoachBaoTriDTO>> GetAllAsync()
        {
            var entities = await _context.KeHoachBaoTris
                .Include(k => k.MaThietBiNavigation)
                .Include(k => k.NguoiTaoNavigation)
                .ToListAsync();
            
            // Manual mapping for each entity
            var dtos = entities.Select(entity => new KeHoachBaoTriDTO
            {
                MaKeHoach = entity.MaKeHoach,
                TieuDe = entity.TieuDe,
                MaThietBi = entity.MaThietBi,
                LoaiBaoTri = entity.LoaiBaoTri,
                ChuKyBaoTri = entity.ChuKyBaoTri,
                NgayBatDau = entity.NgayBatDau,
                NgayKetThuc = entity.NgayKetThuc,
                MoTa = entity.MoTa,
                TrangThai = entity.TrangThai,
                NguoiTao = entity.NguoiTao,
                NgayTao = entity.NgayTao,
                TenThietBi = entity.MaThietBiNavigation?.TenThietBi,
                TenNguoiTao = entity.NguoiTaoNavigation?.TenDangNhap
            });
            
            return dtos;
        }

        public override async Task<KeHoachBaoTriDTO> GetByIdAsync(int id)
        {
            var entity = await _context.KeHoachBaoTris
                .Include(k => k.MaThietBiNavigation)
                .Include(k => k.NguoiTaoNavigation)
                .FirstOrDefaultAsync(k => k.MaKeHoach == id);
            
            if (entity == null) return null;
            
            // Debug: Check if navigation properties are loaded
            System.Diagnostics.Debug.WriteLine($"Before mapping - MaThietBi: {entity.MaThietBi}, TenThietBi: {entity.MaThietBiNavigation?.TenThietBi}");
            System.Diagnostics.Debug.WriteLine($"Before mapping - NguoiTao: {entity.NguoiTao}, TenNguoiTao: {entity.NguoiTaoNavigation?.TenDangNhap}");
            
            // Try manual mapping
            var dto = new KeHoachBaoTriDTO
            {
                MaKeHoach = entity.MaKeHoach,
                TieuDe = entity.TieuDe,
                MaThietBi = entity.MaThietBi,
                LoaiBaoTri = entity.LoaiBaoTri,
                ChuKyBaoTri = entity.ChuKyBaoTri,
                NgayBatDau = entity.NgayBatDau,
                NgayKetThuc = entity.NgayKetThuc,
                MoTa = entity.MoTa,
                TrangThai = entity.TrangThai,
                NguoiTao = entity.NguoiTao,
                NgayTao = entity.NgayTao,
                TenThietBi = entity.MaThietBiNavigation?.TenThietBi,
                TenNguoiTao = entity.NguoiTaoNavigation?.TenDangNhap
            };
            
            // Debug: Check DTO after mapping
            System.Diagnostics.Debug.WriteLine($"After manual mapping - TenThietBi: {dto.TenThietBi}");
            System.Diagnostics.Debug.WriteLine($"After manual mapping - TenNguoiTao: {dto.TenNguoiTao}");
            
            return dto;
        }
    }
}
