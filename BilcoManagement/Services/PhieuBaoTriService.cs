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
            var entities = await _context.PhieuBaoTris
                .Include(pb => pb.MaKeHoachNavigation)
                .Include(pb => pb.MaThietBiNavigation)
                .Include(pb => pb.NhanVienThucHienNavigation)
                .Include(pb => pb.NguoiXacNhanNavigation)
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
                .Include(pb => pb.NhanVienThucHienNavigation)
                .Include(pb => pb.NguoiXacNhanNavigation)
                .FirstOrDefaultAsync(pb => pb.MaPhieu == id);
            
            if (entity == null) return null;
            
            return MapToDTO(entity);
        }

        public override async Task UpdateAsync(int id, UpdatePhieuBaoTriDTO updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Phiếu bảo trì với ID {id} không tồn tại");
            }

            // Kiểm tra các quan hệ nếu có thay đổi
            await ValidateRelationships(updateDto, id);

            // Nếu có người xác nhận, gán ngày xác nhận
            if (updateDto.NguoiXacNhan.HasValue && !entity.NguoiXacNhan.HasValue)
            {
                entity.NgayXacNhan = DateTime.Now;
            }

            _mapper.Map(updateDto, entity);
            await _repository.UpdateAsync(id, entity);
        }

        private async Task LoadNavigationProperties(PhieuBaoTri phieuBaoTri)
        {
            await _context.Entry(phieuBaoTri).Reference(pb => pb.MaKeHoachNavigation).LoadAsync();
            await _context.Entry(phieuBaoTri).Reference(pb => pb.MaThietBiNavigation).LoadAsync();
            await _context.Entry(phieuBaoTri).Reference(pb => pb.NhanVienThucHienNavigation).LoadAsync();
            await _context.Entry(phieuBaoTri).Reference(pb => pb.NguoiXacNhanNavigation).LoadAsync();
        }

        private PhieuBaoTriDTO MapToDTO(PhieuBaoTri entity)
        {
            return new PhieuBaoTriDTO
            {
                MaPhieu = entity.MaPhieu,
                MaKeHoach = entity.MaKeHoach,
                MaThietBi = entity.MaThietBi,
                NhanVienThucHien = entity.NhanVienThucHien,
                ThoiGianBatDau = entity.ThoiGianBatDau,
                ThoiGianKetThuc = entity.ThoiGianKetThuc,
                TinhTrangTruocBT = entity.TinhTrangTruocBT,
                TinhTrangSauBT = entity.TinhTrangSauBT,
                KetQua = entity.KetQua,
                GhiChu = entity.GhiChu,
                NguoiXacNhan = entity.NguoiXacNhan,
                NgayXacNhan = entity.NgayXacNhan,
                TrangThai = entity.TrangThai,
                TenKeHoach = entity.MaKeHoachNavigation?.TieuDe,
                TenThietBi = entity.MaThietBiNavigation?.TenThietBi,
                TenNhanVienThucHien = entity.NhanVienThucHienNavigation?.HoTen,
                TenNguoiXacNhan = entity.NguoiXacNhanNavigation?.HoTen
            };
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

            if (updateDto.NguoiXacNhan.HasValue)
            {
                var nhanVienExists = await _context.NhanViens
                    .AnyAsync(nv => nv.MaNV == updateDto.NguoiXacNhan.Value);
                    
                if (!nhanVienExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy nhân viên xác nhận với mã: {updateDto.NguoiXacNhan}");
                }
            }
        }
    }
}
