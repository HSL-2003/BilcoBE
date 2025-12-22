using Microsoft.AspNetCore.Mvc;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace BilcoManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PhanQuyenController : ControllerBase
    {
        private readonly IPhanQuyenService _phanQuyenService;

        public PhanQuyenController(IPhanQuyenService phanQuyenService)
        {
            _phanQuyenService = phanQuyenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanQuyenDTO>>> GetAll()
        {
            try
            {
                var result = await _phanQuyenService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách quyền", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhanQuyenDTO>> GetById(int id)
        {
            try
            {
                var result = await _phanQuyenService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = $"Không tìm thấy quyền với ID {id}" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy thông tin quyền", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<PhanQuyenDTO>> Create([FromBody] CreatePhanQuyenDTO createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _phanQuyenService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = result.MaQuyen }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi tạo quyền", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePhanQuyenDTO updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _phanQuyenService.UpdateAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật quyền", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Kiểm tra quyền có đang được sử dụng không
                var existing = await _phanQuyenService.GetByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(new { message = $"Không tìm thấy quyền với ID {id}" });
                }

                if (existing.SoLuongNguoiDung > 0)
                {
                    return BadRequest(new { message = "Không thể xóa quyền này vì đang có người dùng sử dụng" });
                }

                await _phanQuyenService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa quyền", error = ex.Message });
            }
        }
    }
}
