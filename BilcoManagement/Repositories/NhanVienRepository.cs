using AutoMapper;
using AutoMapper.QueryableExtensions;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilcoManagement.Repositories
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly QuanLyChatLuongSanPhamContext _context;
        private readonly IMapper _mapper;

        public NhanVienRepository(QuanLyChatLuongSanPhamContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NhanVienDTO>> GetAllNhanViensAsync()
        {
            return await _context.NhanViens
                .ProjectTo<NhanVienDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<NhanVienDTO> GetNhanVienByIdAsync(int id)
        {
            return await _context.NhanViens
                .ProjectTo<NhanVienDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(nv => nv.MaNV == id);
        }

        public async Task<NhanVienDTO> CreateNhanVienAsync(CreateNhanVienDTO nhanVienDto)
        {
            var nhanVien = _mapper.Map<Models.NhanVien>(nhanVienDto);
            nhanVien.NgayTao = DateTime.Now;
            
            _context.NhanViens.Add(nhanVien);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<NhanVienDTO>(nhanVien);
        }

        public async Task UpdateNhanVienAsync(int id, UpdateNhanVienDTO nhanVienDto)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
                throw new KeyNotFoundException("Không tìm thấy nhân viên");

            _mapper.Map(nhanVienDto, nhanVien);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNhanVienAsync(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
                throw new KeyNotFoundException("Không tìm thấy nhân viên");

            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> NhanVienExists(int id)
        {
            return await _context.NhanViens.AnyAsync(e => e.MaNV == id);
        }
    }
}
