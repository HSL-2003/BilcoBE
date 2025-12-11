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
    public class TonKhoController : ControllerBase
    {
        private readonly ITonKhoService _tonKhoService;
        private readonly ILogger<TonKhoController> _logger;

        public TonKhoController(ITonKhoService tonKhoService, ILogger<TonKhoController> logger)
        {
            _tonKhoService = tonKhoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TonKhoDTO>>> GetTonKhos()
        {
            try
            {
                var result = await _tonKhoService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách tồn kho");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TonKhoDTO>> GetTonKho(int id)
        {
            try
            {
                var tonKho = await _tonKhoService.GetByIdAsync(id);
                if (tonKho == null)
                {
                    return NotFound($"Không tìm thấy tồn kho có mã {id}");
                }

                return Ok(tonKho);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy thông tin tồn kho có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TonKhoDTO>> CreateTonKho(CreateTonKhoDTO tonKhoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdTonKho = await _tonKhoService.CreateAsync(tonKhoDto);
                return CreatedAtAction(nameof(GetTonKho), new { id = createdTonKho.MaTonKho }, createdTonKho);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo tồn kho");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi tạo tồn kho");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTonKho(int id, UpdateTonKhoDTO tonKhoDto)
        {
            try
            {
                if (id != tonKhoDto.MaTonKho)
                {
                    return BadRequest("ID không khớp");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _tonKhoService.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy tồn kho có mã {id}");
                }

                await _tonKhoService.UpdateAsync(id, tonKhoDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật tồn kho có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi cập nhật tồn kho");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTonKho(int id)
        {
            try
            {
                if (!await _tonKhoService.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy tồn kho có mã {id}");
                }

                await _tonKhoService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa tồn kho có mã {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xóa tồn kho");
            }
        }
    }
}
