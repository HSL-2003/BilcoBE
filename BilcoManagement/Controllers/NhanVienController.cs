using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BilcoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly ILogger<NhanVienController> _logger;

        public NhanVienController(
            INhanVienRepository nhanVienRepository,
            ILogger<NhanVienController> logger)
        {
            _nhanVienRepository = nhanVienRepository;
            _logger = logger;
        }

        // GET: api/NhanVien
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanVienDTO>>> GetNhanViens()
        {
            try
            {
                var nhanViens = await _nhanVienRepository.GetAllNhanViensAsync();
                return Ok(nhanViens);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách nhân viên");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        // GET: api/NhanVien/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVienDTO>> GetNhanVien(int id)
        {
            try
            {
                var nhanVien = await _nhanVienRepository.GetNhanVienByIdAsync(id);

                if (nhanVien == null)
                {
                    return NotFound($"Không tìm thấy nhân viên có mã {id}");
                }

                return Ok(nhanVien);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy thông tin nhân viên có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        // POST: api/NhanVien
        [HttpPost]
        public async Task<ActionResult<NhanVienDTO>> CreateNhanVien(CreateNhanVienDTO nhanVienDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdNhanVien = await _nhanVienRepository.CreateNhanVienAsync(nhanVienDto);
                return CreatedAtAction(nameof(GetNhanVien), 
                    new { id = createdNhanVien.MaNV }, createdNhanVien);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo mới nhân viên");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi tạo mới nhân viên");
            }
        }

        // PUT: api/NhanVien/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNhanVien(int id, UpdateNhanVienDTO nhanVienDto)
        {
            try
            {
                if (id != nhanVienDto.MaNV)
                {
                    return BadRequest("ID không khớp");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _nhanVienRepository.NhanVienExists(id))
                {
                    return NotFound($"Không tìm thấy nhân viên có mã {id}");
                }

                await _nhanVienRepository.UpdateNhanVienAsync(id, nhanVienDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật nhân viên có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi cập nhật thông tin nhân viên");
            }
        }

        // DELETE: api/NhanVien/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            try
            {
                if (!await _nhanVienRepository.NhanVienExists(id))
                {
                    return NotFound($"Không tìm thấy nhân viên có mã {id}");
                }

                await _nhanVienRepository.DeleteNhanVienAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa nhân viên có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xóa nhân viên");
            }
        }
    }
}
