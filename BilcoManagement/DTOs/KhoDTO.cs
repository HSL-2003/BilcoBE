using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class KhoDTO
    {
        public int MaKho { get; set; }

        [Required(ErrorMessage = "Tên kho không được để trống")]
        [StringLength(100, ErrorMessage = "Tên kho không vượt quá 100 ký tự")]
        public string TenKho { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ không vượt quá 200 ký tự")]
        public string DiaChi { get; set; }

        public int? NguoiQuanLyID { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không vượt quá 500 ký tự")]
        public string GhiChu { get; set; }
    }

    public class CreateKhoDTO
    {
        [Required(ErrorMessage = "Tên kho không được để trống")]
        [StringLength(100, ErrorMessage = "Tên kho không vượt quá 100 ký tự")]
        public string TenKho { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ không vượt quá 200 ký tự")]
        public string DiaChi { get; set; }

        public int? NguoiQuanLyID { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không vượt quá 500 ký tự")]
        public string GhiChu { get; set; }
    }

    public class UpdateKhoDTO : CreateKhoDTO
    {
        [Required]
        public int MaKho { get; set; }
    }
}
