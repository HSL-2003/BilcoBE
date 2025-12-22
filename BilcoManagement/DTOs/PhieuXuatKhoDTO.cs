using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class PhieuXuatKhoDTO
    {
        public int MaPhieuXuat { get; set; }

        [Required]
        [StringLength(50)]
        public string SoPhieu { get; set; }

        public DateTime? NgayXuat { get; set; }

        public int? MaKhoXuat { get; set; }

        public int? MaPhieuBaoTri { get; set; }

        [StringLength(200)]
        public string LyDoXuat { get; set; }

        [StringLength(100)]
        public string NguoiNhanHang { get; set; }

        public int? NguoiLapPhieu { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
        
        // Navigation properties
        public string TenKho { get; set; }
        public string TenPhieuBaoTri { get; set; }
        public string TenNguoiLapPhieu { get; set; }
    }

    public class CreatePhieuXuatKhoDTO
    {
        [Required(ErrorMessage = "Số phiếu không được để trống")]
        [StringLength(50, ErrorMessage = "Số phiếu không vượt quá 50 ký tự")]
        public string SoPhieu { get; set; }

        public DateTime? NgayXuat { get; set; }

        public int? MaKhoXuat { get; set; }

        public int? MaPhieuBaoTri { get; set; }

        [StringLength(200, ErrorMessage = "Lý do xuất không vượt quá 200 ký tự")]
        public string LyDoXuat { get; set; }

        [StringLength(100, ErrorMessage = "Người nhận hàng không vượt quá 100 ký tự")]
        public string NguoiNhanHang { get; set; }

        [StringLength(50, ErrorMessage = "Trạng thái không vượt quá 50 ký tự")]
        public string TrangThai { get; set; } = "Chờ xác nhận";

        [StringLength(500, ErrorMessage = "Ghi chú không vượt quá 500 ký tự")]
        public string GhiChu { get; set; }
    }

    public class UpdatePhieuXuatKhoDTO : CreatePhieuXuatKhoDTO
    {
        [Required]
        public int MaPhieuXuat { get; set; }
    }
}
