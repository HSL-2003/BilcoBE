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
    public class VatTuController : ControllerBase
    {
        private readonly IVatTuService _service;
        private readonly ILogger<VatTuController> _logger;

        public VatTuController(IVatTuService service, ILogger<VatTuController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VatTuDTO>>> GetVatTus()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách vật tư");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VatTuDTO>> GetVatTu(int id)
        {
            try
            {
                var vatTu = await _service.GetByIdAsync(id);
                if (vatTu == null)
                {
                    return NotFound($"Không tìm thấy vật tư có mã {id}");
                }
                return Ok(vatTu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy vật tư {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<VatTuDTO>> CreateVatTu(CreateVatTuDTO vatTuDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var created = await _service.CreateAsync(vatTuDto);
                return CreatedAtAction(nameof(GetVatTu), new { id = created.MaVT }, created);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Dữ liệu tham chiếu không tồn tại");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo vật tư");
                return StatusCode(500, "Đã xảy ra lỗi khi tạo bản ghi");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateVatTu(int id, UpdateVatTuDTO vatTuDto)
        {
            try
            {
                if (id != vatTuDto.MaVT)
                {
                    return BadRequest("ID không khớp");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _service.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy vật tư có mã {id}");
                }

                await _service.UpdateAsync(id, vatTuDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Dữ liệu tham chiếu không tồn tại");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật vật tư {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteVatTu(int id)
        {
            try
            {
                if (!await _service.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy vật tư có mã {id}");
                }

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa vật tư {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }
    }
}
