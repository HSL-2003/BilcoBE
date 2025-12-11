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
    public class DieuChuyenKhoController : ControllerBase
    {
        private readonly IDieuChuyenKhoService _service;
        private readonly ILogger<DieuChuyenKhoController> _logger;

        public DieuChuyenKhoController(IDieuChuyenKhoService service, ILogger<DieuChuyenKhoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DieuChuyenKhoDTO>>> GetAll()
        {
            try
            {
                var data = await _service.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách điều chuyển kho");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DieuChuyenKhoDTO>> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound($"Không tìm thấy điều chuyển kho có mã {id}");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy điều chuyển kho {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xử lý yêu cầu");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DieuChuyenKhoDTO>> Create(CreateDieuChuyenKhoDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.MaDieuChuyen }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo điều chuyển kho");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi tạo bản ghi");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDieuChuyenKhoDTO dto)
        {
            try
            {
                if (id != dto.MaDieuChuyen)
                {
                    return BadRequest("ID không khớp");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _service.ExistsAsync(id))
                {
                    return NotFound($"Không tìm thấy điều chuyển kho có mã {id}");
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
                _logger.LogError(ex, $"Lỗi khi cập nhật điều chuyển kho {id}");
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
                    return NotFound($"Không tìm thấy điều chuyển kho có mã {id}");
                }

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa điều chuyển kho {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi xóa bản ghi");
            }
        }
    }
}
