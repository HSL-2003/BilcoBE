using System;
using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class NguoiDungDTO
    {
        public int MaND { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        public int? MaNV { get; set; }

        public int? MaQuyen { get; set; }

        [StringLength(100)]
        public string PhongBan { get; set; }

        [StringLength(100)]
        public string ChucVu { get; set; }

        public bool? TrangThai { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? LastLogin { get; set; }
    }

    public class CreateNguoiDungDTO
    {
        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string MatKhau { get; set; }

        public int? MaNV { get; set; }

        public int? MaQuyen { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [StringLength(100)]
        public string PhongBan { get; set; }

        [StringLength(100)]
        public string ChucVu { get; set; }

        public bool? TrangThai { get; set; } = true;
    }

    public class UpdateNguoiDungDTO
    {
        [Required]
        public int MaND { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [StringLength(255, MinimumLength = 6)]
        public string MatKhau { get; set; }

        public int? MaNV { get; set; }

        public int? MaQuyen { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [StringLength(100)]
        public string PhongBan { get; set; }

        [StringLength(100)]
        public string ChucVu { get; set; }

        public bool? TrangThai { get; set; }

        public bool? IsActive { get; set; }
    }

    public class RegisterNguoiDungDTO
    {
        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string MatKhau { get; set; }

        public int? MaNV { get; set; }

        public int? MaQuyen { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [StringLength(100)]
        public string PhongBan { get; set; }

        [StringLength(100)]
        public string ChucVu { get; set; }
    }

    public class LoginRequestDTO
    {
        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string MatKhau { get; set; }
    }

    public class AuthResponseDTO
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public int MaND { get; set; }

        public string TenDangNhap { get; set; }

        public int? MaQuyen { get; set; }
    }

    public class ApproveNguoiDungDTO
    {
        [Required]
        public int MaQuyen { get; set; }

        [Required]
        public bool TrangThai { get; set; } = true;

        [Required]
        public bool IsActive { get; set; } = true;

        // Thông tin nhân viên
        public string HoTen { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? ChucVu { get; set; }
        public string? PhongBan { get; set; }
    }

    public class AdminCreateNguoiDungDTO
    {
        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string MatKhau { get; set; }

        public int? MaNV { get; set; }

        [Required]
        public int MaQuyen { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [StringLength(100)]
        public string PhongBan { get; set; }

        [StringLength(100)]
        public string ChucVu { get; set; }

        public bool TrangThai { get; set; } = true;
        public bool IsActive { get; set; } = true;
    }

    public class UpdateNhanVienDto
    {
        public string HoTen { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? ChucVu { get; set; }
        public string? PhongBan { get; set; }
    }

    public class UpdateUserProfileDto
    {
        public string? HoTen { get; set; }
        
        [Phone]
        [StringLength(20)]
        public string? SoDienThoai { get; set; }
        
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }
        
        [StringLength(255, MinimumLength = 6)]
        public string? MatKhau { get; set; }
    }
}
