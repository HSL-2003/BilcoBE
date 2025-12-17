using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class NhaCungCapDTO
    {
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string NguoiLienHe { get; set; }
        public string GhiChu { get; set; }
    }

    public class CreateNhaCungCapDTO
    {
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [StringLength(200, ErrorMessage = "Tên nhà cung cấp không vượt quá 200 ký tự")]
        public string TenNCC { get; set; }

        [StringLength(500, ErrorMessage = "Địa chỉ không vượt quá 500 ký tự")]
        public string DiaChi { get; set; }

        [StringLength(20, ErrorMessage = "Số điện thoại không vượt quá 20 ký tự")]
        [RegularExpression(@"^[\d\s\-\+\(\)]+$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [StringLength(100, ErrorMessage = "Email không vượt quá 100 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "Người liên hệ không vượt quá 100 ký tự")]
        public string NguoiLienHe { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không vượt quá 500 ký tự")]
        public string GhiChu { get; set; }
    }

    public class UpdateNhaCungCapDTO : CreateNhaCungCapDTO
    {
        [Required]
        public int MaNCC { get; set; }
    }
}
