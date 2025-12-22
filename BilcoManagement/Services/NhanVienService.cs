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
    public class NhanVienService : BaseService<NhanVien, NhanVienDTO, CreateNhanVienDTO, UpdateNhanVienDTO>, INhanVienService
    {
        private readonly QuanLyChatLuongSanPhamContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NhanVienService(
     IRepository<NhanVien> repository,
     IMapper mapper,
     QuanLyChatLuongSanPhamContext context,
     IHttpContextAccessor httpContextAccessor)
     : base(repository, mapper)

        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public override async Task<NhanVienDTO> CreateAsync(CreateNhanVienDTO createDto)
        {
            // Kiểm tra email đã tồn tại chưa
            var existingEmail = await _context.NhanViens
                .AnyAsync(nv => nv.Email == createDto.Email);
                
            if (existingEmail)
            {
                throw new ArgumentException($"Email {createDto.Email} đã tồn tại");
            }

            // Kiểm tra UserID có tồn tại không (nếu được cung cấp)
            if (createDto.UserID.HasValue)
            {
                var userExists = await _context.NguoiDungs
                    .AnyAsync(nd => nd.MaND == createDto.UserID.Value);
                    
                if (!userExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy người dùng với ID: {createDto.UserID}");
                }
            }

            // Tạo NhanVien entity và gán ngày tạo
            var nhanVien = _mapper.Map<NhanVien>(createDto);
            nhanVien.NgayTao = DateTime.Now;

            await _repository.AddAsync(nhanVien);
            await _context.SaveChangesAsync();

            // Load navigation properties for DTO mapping
            await _context.Entry(nhanVien).Reference(nv => nv.User).LoadAsync();

            // Manual mapping
            var dto = new NhanVienDTO
            {
                MaNV = nhanVien.MaNV,
                HoTen = nhanVien.HoTen,
                Email = nhanVien.Email,
                SoDienThoai = nhanVien.SoDienThoai,
                ChucVu = nhanVien.ChucVu,
                PhongBan = nhanVien.PhongBan,
                NgayTao = nhanVien.NgayTao,
                TrangThai = nhanVien.TrangThai,
                UserID = nhanVien.UserID,
                TenNguoiDung = nhanVien.User?.TenDangNhap
            };

            return dto;
        }

        public override async Task<IEnumerable<NhanVienDTO>> GetAllAsync()
        {
            var entities = await _context.NhanViens
                .Include(nv => nv.User)
                .ToListAsync();
            
            // Manual mapping for each entity
            var dtos = entities.Select(entity => new NhanVienDTO
            {
                MaNV = entity.MaNV,
                HoTen = entity.HoTen,
                Email = entity.Email,
                SoDienThoai = entity.SoDienThoai,
                ChucVu = entity.ChucVu,
                PhongBan = entity.PhongBan,
                NgayTao = entity.NgayTao,
                TrangThai = entity.TrangThai,
                UserID = entity.UserID,
                TenNguoiDung = entity.User?.TenDangNhap
            });
            
            return dtos;
        }

        public override async Task<NhanVienDTO> GetByIdAsync(int id)
        {
            var entity = await _context.NhanViens
                .Include(nv => nv.User)
                .FirstOrDefaultAsync(nv => nv.MaNV == id);
            
            if (entity == null) return null;
            
            // Manual mapping
            var dto = new NhanVienDTO
            {
                MaNV = entity.MaNV,
                HoTen = entity.HoTen,
                Email = entity.Email,
                SoDienThoai = entity.SoDienThoai,
                ChucVu = entity.ChucVu,
                PhongBan = entity.PhongBan,
                NgayTao = entity.NgayTao,
                TrangThai = entity.TrangThai,
                UserID = entity.UserID,
                TenNguoiDung = entity.User?.TenDangNhap
            };
            
            return dto;
        }

        public override async Task UpdateAsync(int id, UpdateNhanVienDTO updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Nhân viên với ID {id} không tồn tại");
            }

            // Kiểm tra email đã tồn tại chưa (trừ nhân viên hiện tại)
            var existingEmail = await _context.NhanViens
                .AnyAsync(nv => nv.Email == updateDto.Email && nv.MaNV != id);
                
            if (existingEmail)
            {
                throw new ArgumentException($"Email {updateDto.Email} đã tồn tại");
            }

            // Kiểm tra UserID có tồn tại không (nếu được cung cấp và thay đổi)
            if (updateDto.UserID.HasValue && updateDto.UserID.Value != entity.UserID)
            {
                var userExists = await _context.NguoiDungs
                    .AnyAsync(nd => nd.MaND == updateDto.UserID.Value);
                    
                if (!userExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy người dùng với ID: {updateDto.UserID}");
                }
            }

            _mapper.Map(updateDto, entity);
            await _repository.UpdateAsync(id, entity);
        }
    }
}
