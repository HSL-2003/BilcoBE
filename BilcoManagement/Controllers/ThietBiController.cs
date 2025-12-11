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
    public class ThietBiController : ControllerBase
    {
        private readonly ILogger<ThietBiController> _logger;
        private readonly IThietBiService _thietBiService;

        public ThietBiController(IThietBiService thietBiService, ILogger<ThietBiController> logger)
        {
            _thietBiService = thietBiService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThietBiDTO>>> GetAllThietBi()
        {
            try
            {
                var result = await _thietBiService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách thiết bị");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ThietBiDTO>> GetThietBiById(int id)
        {
            try
            {
                var thietBi = await _thietBiService.GetByIdAsync(id);
                if (thietBi == null)
                {
                    return NotFound($"Không tìm thấy thiết bị có mã {id}");
                }

                return Ok(thietBi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy thông tin thiết bị có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }
        [HttpPost]
        public async Task<ActionResult<ThietBiDTO>> CreateThietBi([FromBody] CreateThietBiDTO thietBiDto)
        {
            try
            {
                if (thietBiDto == null)
                {
                    return BadRequest("Dữ liệu thiết bị không hợp lệ");
                }
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (thietBiDto.MaSo == null)
                {
                    return BadRequest("Mã số thiết bị không được để trống");
                }

                var createdThietBi = await _thietBiService.CreateAsync(thietBiDto);
                return CreatedAtAction(nameof(GetThietBiById), new { id = createdThietBi.MaThietBi }, createdThietBi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo thiết bị mới");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateThietBi(int id, [FromBody] UpdateThietBiDTO thietBiDto)
        {
            try
            {
                if (id != thietBiDto.MaThietBi)
                {
                    return BadRequest("ID không khớp");
                }
                if (thietBiDto == null)
                {
                    return BadRequest("Dữ liệu thiết bị không hợp lệ");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _thietBiService.UpdateAsync(id, thietBiDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Không tìm thấy thiết bị có mã {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật thiết bị có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThietBi(int id)
        {
            try
            {
                await _thietBiService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Không tìm thấy thiết bị có mã {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa thiết bị có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }
    }
}
