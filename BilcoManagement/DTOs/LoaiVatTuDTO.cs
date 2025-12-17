using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class LoaiVatTuDTO
    {
        public int MaLoaiVT { get; set; }
        public string TenLoaiVT { get; set; }
        public string MoTa { get; set; }
    }

    public class CreateLoaiVatTuDTO
    {
        [Required(ErrorMessage = "Tên loại vật tư không được để trống")]
        [StringLength(100, ErrorMessage = "Tên loại vật tư không vượt quá 100 ký tự")]
        public string TenLoaiVT { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không vượt quá 500 ký tự")]
        public string MoTa { get; set; }
    }

    public class UpdateLoaiVatTuDTO : CreateLoaiVatTuDTO
    {
        [Required]
        public int MaLoaiVT { get; set; }
    }
}
