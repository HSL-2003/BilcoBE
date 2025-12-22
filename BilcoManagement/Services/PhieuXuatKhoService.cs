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
    public class PhieuXuatKhoService : BaseService<PhieuXuatKho, PhieuXuatKhoDTO, CreatePhieuXuatKhoDTO, UpdatePhieuXuatKhoDTO>, IPhieuXuatKhoService
    {
        private readonly QuanLyChatLuongSanPhamContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhieuXuatKhoService(IPhieuXuatKhoRepository repository, IMapper mapper, QuanLyChatLuongSanPhamContext context, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public override async Task<PhieuXuatKhoDTO> CreateAsync(CreatePhieuXuatKhoDTO createDto)
        {
            // Kiểm tra số phiếu đã tồn tại chưa
            var existingSoPhieu = await _context.PhieuXuatKhos
                .AnyAsync(px => px.SoPhieu == createDto.SoPhieu);
                
            if (existingSoPhieu)
            {
                throw new ArgumentException($"Số phiếu '{createDto.SoPhieu}' đã tồn tại");
            }

            // Kiểm tra kho có tồn tại không
            if (createDto.MaKhoXuat.HasValue)
            {
                var khoExists = await _context.Khos
                    .AnyAsync(k => k.MaKho == createDto.MaKhoXuat.Value);
                    
                if (!khoExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy kho với mã: {createDto.MaKhoXuat}");
                }
            }

            // Kiểm tra phiếu bảo trì có tồn tại không
            if (createDto.MaPhieuBaoTri.HasValue)
            {
                var phieuBaoTriExists = await _context.PhieuBaoTris
                    .AnyAsync(pbt => pbt.MaPhieu == createDto.MaPhieuBaoTri.Value);
                    
                if (!phieuBaoTriExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy phiếu bảo trì với mã: {createDto.MaPhieuBaoTri}");
                }
            }

            // Lấy ID người dùng hiện tại từ token
            var currentUserId = GetCurrentUserId();

            // Tạo PhieuXuatKho entity
            var phieuXuatKho = _mapper.Map<PhieuXuatKho>(createDto);
            phieuXuatKho.NgayXuat = phieuXuatKho.NgayXuat ?? DateTime.Now;
            phieuXuatKho.NguoiLapPhieu = currentUserId;

            await _repository.AddAsync(phieuXuatKho);
            await _context.SaveChangesAsync();

            // Load navigation properties for DTO mapping
            await LoadNavigationProperties(phieuXuatKho);

            // Manual mapping
            var dto = MapToDTO(phieuXuatKho);
            return dto;
        }

        public override async Task<IEnumerable<PhieuXuatKhoDTO>> GetAllAsync()
        {
            var entities = await _context.PhieuXuatKhos
                .Include(px => px.MaKhoXuatNavigation)
                .Include(px => px.MaPhieuBaoTriNavigation)
                .Include(px => px.NguoiLapPhieuNavigation)
                .ToListAsync();
            
            // Manual mapping for each entity
            var dtos = entities.Select(MapToDTO);
            return dtos;
        }

        public override async Task<PhieuXuatKhoDTO> GetByIdAsync(int id)
        {
            var entity = await _context.PhieuXuatKhos
                .Include(px => px.MaKhoXuatNavigation)
                .Include(px => px.MaPhieuBaoTriNavigation)
                .Include(px => px.NguoiLapPhieuNavigation)
                .FirstOrDefaultAsync(px => px.MaPhieuXuat == id);
            
            if (entity == null) return null;
            
            return MapToDTO(entity);
        }

        public override async Task UpdateAsync(int id, UpdatePhieuXuatKhoDTO updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Phiếu xuất kho với ID {id} không tồn tại");
            }

            // Kiểm tra số phiếu đã tồn tại chưa (trừ phiếu hiện tại)
            var existingSoPhieu = await _context.PhieuXuatKhos
                .AnyAsync(px => px.SoPhieu == updateDto.SoPhieu && px.MaPhieuXuat != id);
                
            if (existingSoPhieu)
            {
                throw new ArgumentException($"Số phiếu '{updateDto.SoPhieu}' đã tồn tại");
            }

            // Kiểm tra các quan hệ nếu có thay đổi
            await ValidateRelationships(updateDto);

            _mapper.Map(updateDto, entity);
            await _repository.UpdateAsync(id, entity);
        }

        private async Task LoadNavigationProperties(PhieuXuatKho phieuXuatKho)
        {
            await _context.Entry(phieuXuatKho).Reference(px => px.MaKhoXuatNavigation).LoadAsync();
            await _context.Entry(phieuXuatKho).Reference(px => px.MaPhieuBaoTriNavigation).LoadAsync();
            await _context.Entry(phieuXuatKho).Reference(px => px.NguoiLapPhieuNavigation).LoadAsync();
        }

        private PhieuXuatKhoDTO MapToDTO(PhieuXuatKho entity)
        {
            return new PhieuXuatKhoDTO
            {
                MaPhieuXuat = entity.MaPhieuXuat,
                SoPhieu = entity.SoPhieu,
                NgayXuat = entity.NgayXuat,
                MaKhoXuat = entity.MaKhoXuat,
                MaPhieuBaoTri = entity.MaPhieuBaoTri,
                LyDoXuat = entity.LyDoXuat,
                NguoiNhanHang = entity.NguoiNhanHang,
                NguoiLapPhieu = entity.NguoiLapPhieu,
                TrangThai = entity.TrangThai,
                GhiChu = entity.GhiChu,
                TenKho = entity.MaKhoXuatNavigation?.TenKho,
                TenPhieuBaoTri = entity.MaPhieuBaoTriNavigation != null ? $"PB-{entity.MaPhieuBaoTriNavigation.MaPhieu}" : null,
                TenNguoiLapPhieu = entity.NguoiLapPhieuNavigation?.HoTen
            };
        }

        private async Task ValidateRelationships(UpdatePhieuXuatKhoDTO updateDto)
        {
            if (updateDto.MaKhoXuat.HasValue)
            {
                var khoExists = await _context.Khos
                    .AnyAsync(k => k.MaKho == updateDto.MaKhoXuat.Value);
                    
                if (!khoExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy kho với mã: {updateDto.MaKhoXuat}");
                }
            }

            if (updateDto.MaPhieuBaoTri.HasValue)
            {
                var phieuBaoTriExists = await _context.PhieuBaoTris
                    .AnyAsync(pbt => pbt.MaPhieu == updateDto.MaPhieuBaoTri.Value);
                    
                if (!phieuBaoTriExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy phiếu bảo trì với mã: {updateDto.MaPhieuBaoTri}");
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
