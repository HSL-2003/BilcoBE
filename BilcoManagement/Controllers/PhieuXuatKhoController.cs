using Microsoft.AspNetCore.Mvc;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BilcoManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PhieuXuatKhoController : ControllerBase
    {
        private readonly IPhieuXuatKhoService _phieuXuatKhoService;

        public PhieuXuatKhoController(IPhieuXuatKhoService phieuXuatKhoService)
        {
            _phieuXuatKhoService = phieuXuatKhoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhieuXuatKhoDTO>>> GetAll()
        {
            try
            {
                var result = await _phieuXuatKhoService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách phiếu xuất kho", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhieuXuatKhoDTO>> GetById(int id)
        {
            try
            {
                var result = await _phieuXuatKhoService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = $"Không tìm thấy phiếu xuất kho với ID {id}" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy thông tin phiếu xuất kho", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<PhieuXuatKhoDTO>> Create([FromBody] CreatePhieuXuatKhoDTO createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _phieuXuatKhoService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = result.MaPhieuXuat }, result);
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
                return StatusCode(500, new { message = "Lỗi khi tạo phiếu xuất kho", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePhieuXuatKhoDTO updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _phieuXuatKhoService.UpdateAsync(id, updateDto);
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
                return StatusCode(500, new { message = "Lỗi khi cập nhật phiếu xuất kho", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _phieuXuatKhoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa phiếu xuất kho", error = ex.Message });
            }
        }
    }
}
