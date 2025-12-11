using System;
using System.Threading.Tasks;
using BilcoManagement.DTOs;
using BilcoManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BilcoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterNguoiDungDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _authService.RegisterAsync(registerDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Đăng ký thất bại cho người dùng {User}", registerDto.TenDangNhap);
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đăng ký tài khoản mới");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình đăng ký");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginRequestDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _authService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Đăng nhập thất bại cho người dùng {User}", loginDto.TenDangNhap);
                return Unauthorized("Tên đăng nhập hoặc mật khẩu không đúng");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Tài khoản chưa được duyệt hoặc bị khóa: {User}", loginDto.TenDangNhap);
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đăng nhập");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi trong quá trình đăng nhập");
            }
        }

        [HttpPut("{id}/approve")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<NguoiDungDTO>> Approve(int id, ApproveNguoiDungDTO approveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _authService.ApproveUserAsync(id, approveDto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi duyệt tài khoản {UserId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Không thể duyệt tài khoản lúc này");
            }
        }

        [HttpGet("pending")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<NguoiDungDTO>>> GetPendingUsers()
        {
            try
            {
                var pendingUsers = await _authService.GetPendingUsersAsync();
                return Ok(pendingUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách người dùng chờ duyệt");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi lấy danh sách người dùng chờ duyệt");
            }
        }

        [HttpPost("admin/create")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<NguoiDungDTO>> AdminCreateUser(AdminCreateNguoiDungDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUser = await _authService.AdminCreateUserAsync(createDto);
                return CreatedAtAction(nameof(AdminCreateUser), new { id = createdUser.MaND }, createdUser);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Lỗi khi tạo tài khoản: {Message}", ex.Message);
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Dữ liệu không hợp lệ: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo tài khoản mới");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi tạo tài khoản");
            }
        }
        [HttpGet("users")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<NguoiDungDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _authService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách tất cả người dùng");
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi khi lấy danh sách người dùng");
            }
        }

        [HttpPut("nhanvien/{maNV}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateNhanVien(int maNV, [FromBody] UpdateNhanVienDto updateDto)
        {
            try
            {
                var result = await _authService.UpdateNhanVienInfoAsync(maNV, updateDto);
                return result ? NoContent() : StatusCode(500, "Có lỗi xảy ra khi cập nhật thông tin nhân viên");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật thông tin nhân viên");
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật thông tin nhân viên");
            }
        }
        [HttpGet("debug-token")]
        [Authorize]
        public IActionResult DebugToken()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(new
            {
                Claims = claims,
                NameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Role = User.FindFirst(ClaimTypes.Role)?.Value,
                Username = User.Identity.Name,
                IsAuthenticated = User.Identity.IsAuthenticated
            });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<NguoiDungDTO>> GetUserByID(int id)
        {
            try
            {
                // Lấy userId từ token JWT
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentUserId))
                {
                    return Unauthorized("Không xác định được người dùng");
                }
                
                // Lấy role của người dùng hiện tại
                var roleClaim = User.FindFirst(ClaimTypes.Role);
                var isAdmin = roleClaim?.Value == "1";

                // Chỉ cho phép xem thông tin nếu là admin hoặc xem thông tin của chính mình
                if (!isAdmin && currentUserId != id)
                {
                    return Forbid("Bạn không có quyền xem thông tin của người dùng khác");
                }

                var user = await _authService.GetUserByIDAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin người dùng theo ID");
                return StatusCode(500, "Đã xảy ra lỗi khi lấy thông tin người dùng");
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<NguoiDungDTO>> DeteteUser(int id)
        {
            try
            {
                var deletedUser = await _authService.DeleteUserByIdAsync(id);
                return Ok(deletedUser);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa người dùng theo ID");
                return StatusCode(500, "Đã xảy ra lỗi khi xóa người dùng");
            }
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult<NguoiDungDTO>> UpdateProfile([FromBody] UpdateUserProfileDto updateDto)
        {
            try
            {
                // Lấy userId từ token JWT
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized("Không xác định được người dùng");
                }

                var updatedUser = await _authService.UpdateUserProfileAsync(userId, updateDto);
                return Ok(updatedUser);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật thông tin cá nhân");
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật thông tin cá nhân");
            }
        }
        
    }
}
