using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class PhanQuyenDTO
    {
        public int MaQuyen { get; set; }

        [Required]
        [StringLength(50)]
        public string TenQuyen { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }
        
        // Navigation properties
        public int? SoLuongNguoiDung { get; set; }
    }

    public class CreatePhanQuyenDTO
    {
        [Required(ErrorMessage = "Tên quyền không được để trống")]
        [StringLength(50, ErrorMessage = "Tên quyền không vượt quá 50 ký tự")]
        public string TenQuyen { get; set; }

        [StringLength(200, ErrorMessage = "Mô tả không vượt quá 200 ký tự")]
        public string MoTa { get; set; }
    }

    public class UpdatePhanQuyenDTO : CreatePhanQuyenDTO
    {
        [Required]
        public int MaQuyen { get; set; }
    }
}
