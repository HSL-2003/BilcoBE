using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BilcoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoaiVatTuController : ControllerBase
    {
        private readonly ILoaiVatTuService _service;
        private readonly ILogger<LoaiVatTuController> _logger;

        public LoaiVatTuController(ILoaiVatTuService service, ILogger<LoaiVatTuController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiVatTuDTO>>> GetLoaiVatTus()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách loại vật tư");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiVatTuDTO>> GetLoaiVatTu(int id)
        {
            try
            {
                var loaiVatTu = await _service.GetByIdAsync(id);
                if (loaiVatTu == null)
                {
                    return NotFound($"Không tìm thấy loại vật tư có mã {id}");
                }
                return Ok(loaiVatTu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy loại vật tư {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<LoaiVatTuDTO>> CreateLoaiVatTu(CreateLoaiVatTuDTO loaiVatTuDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var created = await _service.CreateAsync(loaiVatTuDto);
                return CreatedAtAction(nameof(GetLoaiVatTu), new { id = created.MaLoaiVT }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo loại vật tư");
                return StatusCode(500, "Đã xảy ra lỗi khi tạo bản ghi");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateLoaiVatTu(int id, UpdateLoaiVatTuDTO loaiVatTuDto)
        {
            try
            {
                if (id != loaiVatTuDto.MaLoaiVT)
                {
                    return BadRequest("ID không khớp");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _service.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy loại vật tư có mã {id}");
                }

                await _service.UpdateAsync(id, loaiVatTuDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật loại vật tư {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteLoaiVatTu(int id)
        {
            try
            {
                if (!await _service.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy loại vật tư có mã {id}");
                }

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa loại vật tư {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }
    }
}
