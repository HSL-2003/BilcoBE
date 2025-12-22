using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class NhanVienDTO
    {
        public int MaNV { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không vượt quá 100 ký tự")]
        public string HoTen { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không vượt quá 100 ký tự")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        public string SoDienThoai { get; set; }

        [StringLength(50, ErrorMessage = "Chức vụ không vượt quá 50 ký tự")]
        public string ChucVu { get; set; }

        [StringLength(50, ErrorMessage = "Phòng ban không vượt quá 50 ký tự")]
        public string PhongBan { get; set; }

        public DateTime? NgayTao { get; set; }

        public bool? TrangThai { get; set; } = true;

        public int? UserID { get; set; }
        
        // Navigation properties
        public string TenNguoiDung { get; set; }
    }

    public class CreateNhanVienDTO
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không vượt quá 100 ký tự")]
        public string HoTen { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không vượt quá 100 ký tự")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        public string SoDienThoai { get; set; }

        [StringLength(50, ErrorMessage = "Chức vụ không vượt quá 50 ký tự")]
        public string ChucVu { get; set; }

        [StringLength(50, ErrorMessage = "Phòng ban không vượt quá 50 ký tự")]
        public string PhongBan { get; set; }

        public bool? TrangThai { get; set; } = true;

        public int? UserID { get; set; }
    }

    public class UpdateNhanVienDTO : CreateNhanVienDTO
    {
        [Required]
        public int MaNV { get; set; }
    }
}
