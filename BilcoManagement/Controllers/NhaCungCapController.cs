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
    public class NhaCungCapController : ControllerBase
    {
        private readonly INhaCungCapService _service;
        private readonly ILogger<NhaCungCapController> _logger;

        public NhaCungCapController(INhaCungCapService service, ILogger<NhaCungCapController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhaCungCapDTO>>> GetNhaCungCaps()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách nhà cung cấp");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NhaCungCapDTO>> GetNhaCungCap(int id)
        {
            try
            {
                var nhaCungCap = await _service.GetByIdAsync(id);
                if (nhaCungCap == null)
                {
                    return NotFound($"Không tìm thấy nhà cung cấp có mã {id}");
                }
                return Ok(nhaCungCap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy nhà cung cấp {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<NhaCungCapDTO>> CreateNhaCungCap(CreateNhaCungCapDTO nhaCungCapDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var created = await _service.CreateAsync(nhaCungCapDto);
                return CreatedAtAction(nameof(GetNhaCungCap), new { id = created.MaNCC }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo nhà cung cấp");
                return StatusCode(500, "Đã xảy ra lỗi khi tạo bản ghi");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateNhaCungCap(int id, UpdateNhaCungCapDTO nhaCungCapDto)
        {
            try
            {
                if (id != nhaCungCapDto.MaNCC)
                {
                    return BadRequest("ID không khớp");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _service.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy nhà cung cấp có mã {id}");
                }

                await _service.UpdateAsync(id, nhaCungCapDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật nhà cung cấp {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteNhaCungCap(int id)
        {
            try
            {
                if (!await _service.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy nhà cung cấp có mã {id}");
                }

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa nhà cung cấp {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }
    }
}
