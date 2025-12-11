using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BilcoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietDieuChuyenController : ControllerBase
    {
        private readonly IChiTietDieuChuyenService _service;
        private readonly ILogger<ChiTietDieuChuyenController> _logger;

        public ChiTietDieuChuyenController(IChiTietDieuChuyenService service, ILogger<ChiTietDieuChuyenController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietDieuChuyenDTO>>> GetAll()
        {
            try
            {
                var data = await _service.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách chi tiết điều chuyển");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTietDieuChuyenDTO>> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound($"Không tìm thấy chi tiết điều chuyển có mã {id}");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy chi tiết điều chuyển {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ChiTietDieuChuyenDTO>> Create(CreateChiTietDieuChuyenDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.MaCTDC }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo chi tiết điều chuyển");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi tạo bản ghi");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateChiTietDieuChuyenDTO dto)
        {
            try
            {
                if (id != dto.MaCTDC)
                {
                    return BadRequest("ID không khớp");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _service.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy chi tiết điều chuyển có mã {id}");
                }

                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật chi tiết điều chuyển {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi cập nhật bản ghi");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await _service.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy chi tiết điều chuyển có mã {id}");
                }

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa chi tiết điều chuyển {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xóa bản ghi");
            }
        }
    }
}
