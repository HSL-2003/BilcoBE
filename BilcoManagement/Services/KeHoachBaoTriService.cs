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

            var dto = _mapper.Map<KeHoachBaoTriDTO>(keHoachBaoTri);
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
            
            return _mapper.Map<IEnumerable<KeHoachBaoTriDTO>>(entities);
        }

        public override async Task<KeHoachBaoTriDTO> GetByIdAsync(int id)
        {
            var entity = await _context.KeHoachBaoTris
                .Include(k => k.MaThietBiNavigation)
                .Include(k => k.NguoiTaoNavigation)
                .FirstOrDefaultAsync(k => k.MaKeHoach == id);
            
            if (entity == null) return null;
            
            return _mapper.Map<KeHoachBaoTriDTO>(entity);
        }
    }
}
