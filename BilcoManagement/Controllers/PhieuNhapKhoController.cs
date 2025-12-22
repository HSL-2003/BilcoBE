using Microsoft.AspNetCore.Mvc;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BilcoManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PhieuNhapKhoController : ControllerBase
    {
        private readonly IPhieuNhapKhoService _phieuNhapKhoService;

        public PhieuNhapKhoController(IPhieuNhapKhoService phieuNhapKhoService)
        {
            _phieuNhapKhoService = phieuNhapKhoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhieuNhapKhoDTO>>> GetAll()
        {
            try
            {
                var result = await _phieuNhapKhoService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách phiếu nhập kho", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhieuNhapKhoDTO>> GetById(int id)
        {
            try
            {
                var result = await _phieuNhapKhoService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = $"Không tìm thấy phiếu nhập kho với ID {id}" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy thông tin phiếu nhập kho", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<PhieuNhapKhoDTO>> Create([FromBody] CreatePhieuNhapKhoDTO createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _phieuNhapKhoService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = result.MaPhieuNhap }, result);
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
                return StatusCode(500, new { message = "Lỗi khi tạo phiếu nhập kho", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePhieuNhapKhoDTO updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _phieuNhapKhoService.UpdateAsync(id, updateDto);
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
                return StatusCode(500, new { message = "Lỗi khi cập nhật phiếu nhập kho", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _phieuNhapKhoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa phiếu nhập kho", error = ex.Message });
            }
        }
    }
}
