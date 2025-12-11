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
    public class KhoController : ControllerBase
    {
        private readonly IKhoService _khoService;
        private readonly ILogger<KhoController> _logger;

        public KhoController(IKhoService khoService, ILogger<KhoController> logger)
        {
            _khoService = khoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhoDTO>>> GetKhos()
        {
            try
            {
                var result = await _khoService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách kho");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KhoDTO>> GetKho(int id)
        {
            try
            {
                var kho = await _khoService.GetByIdAsync(id);
                if (kho == null)
                {
                    return NotFound($"Không tìm thấy kho có mã {id}");
                }

                return Ok(kho);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy thông tin kho có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpPost]
        public async Task<ActionResult<KhoDTO>> CreateKho(CreateKhoDTO khoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdKho = await _khoService.CreateAsync(khoDto);
                return CreatedAtAction(nameof(GetKho), new { id = createdKho.MaKho }, createdKho);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo mới kho");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi tạo kho");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKho(int id, UpdateKhoDTO khoDto)
        {
            try
            {
                if (id != khoDto.MaKho)
                {
                    return BadRequest("ID không khớp");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _khoService.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy kho có mã {id}");
                }

                await _khoService.UpdateAsync(id, khoDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật kho có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi cập nhật kho");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKho(int id)
        {
            try
            {
                if (!await _khoService.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy kho có mã {id}");
                }

                await _khoService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa kho có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xóa kho");
            }
        }
    }
}
