using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
using Microsoft.EntityFrameworkCore;
using BilcoManagement.Repositories;

namespace BilcoManagement.Services
{
    public class PhanQuyenService : BaseService<PhanQuyen, PhanQuyenDTO, CreatePhanQuyenDTO, UpdatePhanQuyenDTO>, IPhanQuyenService
    {
        private readonly QuanLyChatLuongSanPhamContext _context;

        public PhanQuyenService(IPhanQuyenRepository repository, IMapper mapper, QuanLyChatLuongSanPhamContext context)
            : base(repository, mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override async Task<PhanQuyenDTO> CreateAsync(CreatePhanQuyenDTO createDto)
        {
            // Kiểm tra tên quyền đã tồn tại chưa
            var existingQuyen = await _context.PhanQuyens
                .AnyAsync(pq => pq.TenQuyen == createDto.TenQuyen);
                
            if (existingQuyen)
            {
                throw new ArgumentException($"Quyền '{createDto.TenQuyen}' đã tồn tại");
            }

            var phanQuyen = _mapper.Map<PhanQuyen>(createDto);
            await _repository.AddAsync(phanQuyen);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<PhanQuyenDTO>(phanQuyen);
            
            // Load số lượng người dùng
            await LoadSoLuongNguoiDung(dto);
            
            return dto;
        }

        public override async Task<IEnumerable<PhanQuyenDTO>> GetAllAsync()
        {
            var entities = await _context.PhanQuyens
                .Include(pq => pq.NguoiDungs)
                .ToListAsync();
            
            var dtos = _mapper.Map<IEnumerable<PhanQuyenDTO>>(entities);
            
            // Load số lượng người dùng cho mỗi quyền
            foreach (var dto in dtos)
            {
                await LoadSoLuongNguoiDung(dto);
            }
            
            return dtos;
        }

        public override async Task<PhanQuyenDTO> GetByIdAsync(int id)
        {
            var entity = await _context.PhanQuyens
                .Include(pq => pq.NguoiDungs)
                .FirstOrDefaultAsync(pq => pq.MaQuyen == id);
            
            if (entity == null) return null;
            
            var dto = _mapper.Map<PhanQuyenDTO>(entity);
            
            // Load số lượng người dùng
            await LoadSoLuongNguoiDung(dto);
            
            return dto;
        }

        public override async Task UpdateAsync(int id, UpdatePhanQuyenDTO updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Quyền với ID {id} không tồn tại");
            }

            // Kiểm tra tên quyền đã tồn tại chưa (trừ quyền hiện tại)
            var existingQuyen = await _context.PhanQuyens
                .AnyAsync(pq => pq.TenQuyen == updateDto.TenQuyen && pq.MaQuyen != id);
                
            if (existingQuyen)
            {
                throw new ArgumentException($"Quyền '{updateDto.TenQuyen}' đã tồn tại");
            }

            _mapper.Map(updateDto, entity);
            await _repository.UpdateAsync(id, entity);
        }

        private async Task LoadSoLuongNguoiDung(PhanQuyenDTO dto)
        {
            var soLuong = await _context.NguoiDungs
                .CountAsync(nd => nd.MaQuyen == dto.MaQuyen);
            dto.SoLuongNguoiDung = soLuong;
        }
    }
}
