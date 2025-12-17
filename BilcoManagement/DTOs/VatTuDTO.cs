using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class VatTuDTO
    {
        public int MaVT { get; set; }
        public int? MaLoaiVT { get; set; }
        public int? MaDVT { get; set; }
        public string TenVT { get; set; }
        public string MaVach { get; set; }
        public int? MaNCC { get; set; }
        public int? ThoiGianBaoHanh { get; set; }
        public string GhiChu { get; set; }
        public string HinhAnh { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? NguoiTao { get; set; }
        
        // Navigation properties
        public string TenLoaiVT { get; set; }
        public string TenDVT { get; set; }
        public string TenNCC { get; set; }
        public string TenNguoiTao { get; set; }
    }

    public class CreateVatTuDTO
    {
        [Required(ErrorMessage = "Tên vật tư không được để trống")]
        [StringLength(200, ErrorMessage = "Tên vật tư không vượt quá 200 ký tự")]
        public string TenVT { get; set; }

        [StringLength(50, ErrorMessage = "Mã vạch không vượt quá 50 ký tự")]
        public string MaVach { get; set; }

        public int? MaLoaiVT { get; set; }
        public int? MaDVT { get; set; }
        public int? MaNCC { get; set; }
        public int? ThoiGianBaoHanh { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không vượt quá 500 ký tự")]
        public string GhiChu { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình ảnh không vượt quá 255 ký tự")]
        public string HinhAnh { get; set; }
    }

    public class UpdateVatTuDTO : CreateVatTuDTO
    {
        [Required]
        public int MaVT { get; set; }
    }
}
