using Microsoft.AspNetCore.Mvc;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BilcoManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PhieuBaoTriController : ControllerBase
    {
        private readonly IPhieuBaoTriService _phieuBaoTriService;

        public PhieuBaoTriController(IPhieuBaoTriService phieuBaoTriService)
        {
            _phieuBaoTriService = phieuBaoTriService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhieuBaoTriDTO>>> GetAll()
        {
            try
            {
                var result = await _phieuBaoTriService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách phiếu bảo trì", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhieuBaoTriDTO>> GetById(int id)
        {
            try
            {
                var result = await _phieuBaoTriService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = $"Không tìm thấy phiếu bảo trì với ID {id}" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy thông tin phiếu bảo trì", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<PhieuBaoTriDTO>> Create([FromBody] CreatePhieuBaoTriDTO createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _phieuBaoTriService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = result.MaPhieu }, result);
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
                return StatusCode(500, new { message = "Lỗi khi tạo phiếu bảo trì", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePhieuBaoTriDTO updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _phieuBaoTriService.UpdateAsync(id, updateDto);
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
                return StatusCode(500, new { message = "Lỗi khi cập nhật phiếu bảo trì", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _phieuBaoTriService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa phiếu bảo trì", error = ex.Message });
            }
        }
    }
}
