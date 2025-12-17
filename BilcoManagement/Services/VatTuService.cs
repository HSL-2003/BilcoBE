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
    public class VatTuService : BaseService<VatTu, VatTuDTO, CreateVatTuDTO, UpdateVatTuDTO>, IVatTuService
    {
        private readonly QuanLyChatLuongSanPhamContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VatTuService(IVatTuRepository repository, IMapper mapper, QuanLyChatLuongSanPhamContext context, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public override async Task<VatTuDTO> CreateAsync(CreateVatTuDTO createDto)
        {
            // Kiểm tra loại vật tư có tồn tại không
            if (createDto.MaLoaiVT.HasValue)
            {
                var loaiVTExists = await _context.LoaiVatTus
                    .AnyAsync(lvt => lvt.MaLoaiVT == createDto.MaLoaiVT.Value);
                    
                if (!loaiVTExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy loại vật tư với mã: {createDto.MaLoaiVT}");
                }
            }

            // Kiểm tra đơn vị tính có tồn tại không
            if (createDto.MaDVT.HasValue)
            {
                var dvtExists = await _context.DonViTinhs
                    .AnyAsync(dvt => dvt.MaDVT == createDto.MaDVT.Value);
                    
                if (!dvtExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy đơn vị tính với mã: {createDto.MaDVT}");
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
            
            // Tạo VatTu entity và gán người tạo
            var vatTu = _mapper.Map<VatTu>(createDto);
            vatTu.NguoiTao = currentUserId;
            vatTu.NgayTao = DateTime.Now;

            await _repository.AddAsync(vatTu);
            await _context.SaveChangesAsync();

            // Load navigation properties for DTO mapping
            await _context.Entry(vatTu).Reference(v => v.MaLoaiVTNavigation).LoadAsync();
            await _context.Entry(vatTu).Reference(v => v.MaDVTNavigation).LoadAsync();
            await _context.Entry(vatTu).Reference(v => v.MaNCCNavigation).LoadAsync();
            await _context.Entry(vatTu).Reference(v => v.NguoiTaoNavigation).LoadAsync();

            // Debug logging
            System.Diagnostics.Debug.WriteLine($"Created - MaLoaiVT: {vatTu.MaLoaiVT}, TenLoaiVT: {vatTu.MaLoaiVTNavigation?.TenLoaiVT}");
            System.Diagnostics.Debug.WriteLine($"Created - MaDVT: {vatTu.MaDVT}, TenDVT: {vatTu.MaDVTNavigation?.TenDVT}");
            System.Diagnostics.Debug.WriteLine($"Created - MaNCC: {vatTu.MaNCC}, TenNCC: {vatTu.MaNCCNavigation?.TenNCC}");
            System.Diagnostics.Debug.WriteLine($"Created - NguoiTao: {vatTu.NguoiTao}, TenNguoiTao: {vatTu.NguoiTaoNavigation?.TenDangNhap}");

            // Try manual mapping as fallback
            var dto = new VatTuDTO
            {
                MaVT = vatTu.MaVT,
                MaLoaiVT = vatTu.MaLoaiVT,
                MaDVT = vatTu.MaDVT,
                TenVT = vatTu.TenVT,
                MaVach = vatTu.MaVach,
                MaNCC = vatTu.MaNCC,
                ThoiGianBaoHanh = vatTu.ThoiGianBaoHanh,
                GhiChu = vatTu.GhiChu,
                HinhAnh = vatTu.HinhAnh,
                NgayTao = vatTu.NgayTao,
                NguoiTao = vatTu.NguoiTao,
                TenLoaiVT = vatTu.MaLoaiVTNavigation?.TenLoaiVT,
                TenDVT = vatTu.MaDVTNavigation?.TenDVT,
                TenNCC = vatTu.MaNCCNavigation?.TenNCC,
                TenNguoiTao = vatTu.NguoiTaoNavigation?.TenDangNhap
            };
            
            // Debug after manual mapping
            System.Diagnostics.Debug.WriteLine($"After manual mapping - TenLoaiVT: {dto.TenLoaiVT}");
            System.Diagnostics.Debug.WriteLine($"After manual mapping - TenDVT: {dto.TenDVT}");
            System.Diagnostics.Debug.WriteLine($"After manual mapping - TenNCC: {dto.TenNCC}");
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

        public override async Task<IEnumerable<VatTuDTO>> GetAllAsync()
        {
            var entities = await _context.VatTus
                .Include(v => v.MaLoaiVTNavigation)
                .Include(v => v.MaDVTNavigation)
                .Include(v => v.MaNCCNavigation)
                .Include(v => v.NguoiTaoNavigation)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<VatTuDTO>>(entities);
        }

        public override async Task<VatTuDTO> GetByIdAsync(int id)
        {
            var entity = await _context.VatTus
                .Include(v => v.MaLoaiVTNavigation)
                .Include(v => v.MaDVTNavigation)
                .Include(v => v.MaNCCNavigation)
                .Include(v => v.NguoiTaoNavigation)
                .FirstOrDefaultAsync(v => v.MaVT == id);
            
            if (entity == null) return null;
            
            // Debug: Check if navigation properties are loaded
            System.Diagnostics.Debug.WriteLine($"Before mapping - MaLoaiVT: {entity.MaLoaiVT}, TenLoaiVT: {entity.MaLoaiVTNavigation?.TenLoaiVT}");
            System.Diagnostics.Debug.WriteLine($"Before mapping - MaDVT: {entity.MaDVT}, TenDVT: {entity.MaDVTNavigation?.TenDVT}");
            System.Diagnostics.Debug.WriteLine($"Before mapping - MaNCC: {entity.MaNCC}, TenNCC: {entity.MaNCCNavigation?.TenNCC}");
            System.Diagnostics.Debug.WriteLine($"Before mapping - NguoiTao: {entity.NguoiTao}, TenNguoiTao: {entity.NguoiTaoNavigation?.TenDangNhap}");
            
            var dto = _mapper.Map<VatTuDTO>(entity);
            
            // Debug: Check DTO after mapping
            System.Diagnostics.Debug.WriteLine($"After mapping - TenLoaiVT: {dto.TenLoaiVT}");
            System.Diagnostics.Debug.WriteLine($"After mapping - TenDVT: {dto.TenDVT}");
            System.Diagnostics.Debug.WriteLine($"After mapping - TenNCC: {dto.TenNCC}");
            System.Diagnostics.Debug.WriteLine($"After mapping - TenNguoiTao: {dto.TenNguoiTao}");
            
            return dto;
        }
    }
}
