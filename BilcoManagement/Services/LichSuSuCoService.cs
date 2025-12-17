using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using BilcoManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BilcoManagement.Services
{
    public class LichSuSuCoService : BaseService<LichSuSuCo, LichSuSuCoDTO, CreateLichSuSuCoDTO, UpdateLichSuSuCoDTO>, ILichSuSuCoService
    {
        private readonly QuanLyChatLuongSanPhamContext _context;

        public LichSuSuCoService(ILichSuSuCoRepository repository, IMapper mapper, QuanLyChatLuongSanPhamContext context)
            : base(repository, mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override async Task<LichSuSuCoDTO> CreateAsync(CreateLichSuSuCoDTO createDto)
        {
            // Kiểm tra người dùng có tồn tại không
            if (createDto.NguoiDungID.HasValue)
            {
                var nguoiDungExists = await _context.NguoiDungs
                    .AnyAsync(nd => nd.MaND == createDto.NguoiDungID.Value);
                    
                if (!nguoiDungExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy người dùng với ID: {createDto.NguoiDungID}");
                }
            }

            // Kiểm tra thiết bị nếu có
            if (createDto.MaThietBi.HasValue)
            {
                var thietBiExists = await _context.ThietBis
                    .AnyAsync(tb => tb.MaThietBi == createDto.MaThietBi.Value);
                    
                if (!thietBiExists)
                {
                    throw new KeyNotFoundException($"Không tìm thấy thiết bị với ID: {createDto.MaThietBi}");
                }
            }

            return await base.CreateAsync(createDto);
        }
    }
}
