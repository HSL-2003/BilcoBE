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
    public class PhieuNhapKhoService : BaseService<PhieuNhapKho, PhieuNhapKhoDTO, CreatePhieuNhapKhoDTO, UpdatePhieuNhapKhoDTO>, IPhieuNhapKhoService
    {
        private readonly QuanLyChatLuongSanPhamContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhieuNhapKhoService(IPhieuNhapKhoRepository repository, IMapper mapper, QuanLyChatLuongSanPhamContext context, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public override async Task<PhieuNhapKhoDTO> CreateAsync(CreatePhieuNhapKhoDTO createDto)
        {
            // Kiểm tra số phiếu đã tồn tại chưa
            var existingSoPhieu = await _context.PhieuNhapKhos
                .AnyAsync(pn => pn.SoPhieu == createDto.SoPhieu);
                
            if (existingSoPhieu)
            {
                throw new ArgumentException($"Số phiếu '{createDto.SoPhieu}' đã tồn tại");
            }

            // Kiểm tra kho có tồn tại không
            if (createDto.MaKhoNhap.HasValue)
            {
                var khoExists = await _context.Khos
                    .AnyAsync(k => k.MaKho == createDto.MaKhoNhap.Value);
                    
                if (!khoExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy kho với mã: {createDto.MaKhoNhap}");
                }
            }

            // Kiểm tra nhà cung cấp có tồn tại không
            if (createDto.MaNCC.HasValue)
            {
                var nccExists = await _context.NhaCungCaps
                    .AnyAsync(ncc => ncc.MaNCC == createDto.MaNCC.Value);
                    
                if (!nccExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy nhà cung cấp với mã: {createDto.MaNCC}");
                }
            }

            // Lấy ID người dùng hiện tại từ token
            var currentUserId = GetCurrentUserId();

            // Tạo PhieuNhapKho entity
            var phieuNhapKho = _mapper.Map<PhieuNhapKho>(createDto);
            phieuNhapKho.NgayNhap = phieuNhapKho.NgayNhap ?? DateTime.Now;
            phieuNhapKho.NguoiLapPhieu = currentUserId;

            await _repository.AddAsync(phieuNhapKho);
            await _context.SaveChangesAsync();

            // Load navigation properties for DTO mapping
            await LoadNavigationProperties(phieuNhapKho);

            // Manual mapping
            var dto = MapToDTO(phieuNhapKho);
            return dto;
        }

        public override async Task<IEnumerable<PhieuNhapKhoDTO>> GetAllAsync()
        {
            var entities = await _context.PhieuNhapKhos
                .Include(pn => pn.MaKhoNhapNavigation)
                .Include(pn => pn.MaNCCNavigation)
                .Include(pn => pn.NguoiLapPhieuNavigation)
                .ToListAsync();
            
            // Manual mapping for each entity
            var dtos = entities.Select(MapToDTO);
            return dtos;
        }

        public override async Task<PhieuNhapKhoDTO> GetByIdAsync(int id)
        {
            var entity = await _context.PhieuNhapKhos
                .Include(pn => pn.MaKhoNhapNavigation)
                .Include(pn => pn.MaNCCNavigation)
                .Include(pn => pn.NguoiLapPhieuNavigation)
                .FirstOrDefaultAsync(pn => pn.MaPhieuNhap == id);
            
            if (entity == null) return null;
            
            return MapToDTO(entity);
        }

        public override async Task UpdateAsync(int id, UpdatePhieuNhapKhoDTO updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Phiếu nhập kho với ID {id} không tồn tại");
            }

            // Kiểm tra số phiếu đã tồn tại chưa (trừ phiếu hiện tại)
            var existingSoPhieu = await _context.PhieuNhapKhos
                .AnyAsync(pn => pn.SoPhieu == updateDto.SoPhieu && pn.MaPhieuNhap != id);
                
            if (existingSoPhieu)
            {
                throw new ArgumentException($"Số phiếu '{updateDto.SoPhieu}' đã tồn tại");
            }

            // Kiểm tra các quan hệ nếu có thay đổi
            await ValidateRelationships(updateDto);

            _mapper.Map(updateDto, entity);
            await _repository.UpdateAsync(id, entity);
        }

        private async Task LoadNavigationProperties(PhieuNhapKho phieuNhapKho)
        {
            await _context.Entry(phieuNhapKho).Reference(pn => pn.MaKhoNhapNavigation).LoadAsync();
            await _context.Entry(phieuNhapKho).Reference(pn => pn.MaNCCNavigation).LoadAsync();
            await _context.Entry(phieuNhapKho).Reference(pn => pn.NguoiLapPhieuNavigation).LoadAsync();
        }

        private PhieuNhapKhoDTO MapToDTO(PhieuNhapKho entity)
        {
            return new PhieuNhapKhoDTO
            {
                MaPhieuNhap = entity.MaPhieuNhap,
                SoPhieu = entity.SoPhieu,
                NgayNhap = entity.NgayNhap,
                MaKhoNhap = entity.MaKhoNhap,
                MaNCC = entity.MaNCC,
                NguoiGiaoHang = entity.NguoiGiaoHang,
                SoHoaDon = entity.SoHoaDon,
                NgayHoaDon = entity.NgayHoaDon,
                TongTien = entity.TongTien,
                GhiChu = entity.GhiChu,
                NguoiLapPhieu = entity.NguoiLapPhieu,
                TrangThai = entity.TrangThai,
                TenKho = entity.MaKhoNhapNavigation?.TenKho,
                TenNCC = entity.MaNCCNavigation?.TenNCC,
                TenNguoiLapPhieu = entity.NguoiLapPhieuNavigation?.HoTen
            };
        }

        private async Task ValidateRelationships(UpdatePhieuNhapKhoDTO updateDto)
        {
            if (updateDto.MaKhoNhap.HasValue)
            {
                var khoExists = await _context.Khos
                    .AnyAsync(k => k.MaKho == updateDto.MaKhoNhap.Value);
                    
                if (!khoExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy kho với mã: {updateDto.MaKhoNhap}");
                }
            }

            if (updateDto.MaNCC.HasValue)
            {
                var nccExists = await _context.NhaCungCaps
                    .AnyAsync(ncc => ncc.MaNCC == updateDto.MaNCC.Value);
                    
                if (!nccExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy nhà cung cấp với mã: {updateDto.MaNCC}");
                }
            }
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
    }
}
