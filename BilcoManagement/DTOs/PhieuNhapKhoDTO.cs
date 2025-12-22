using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class PhieuNhapKhoDTO
    {
        public int MaPhieuNhap { get; set; }

        [Required]
        [StringLength(50)]
        public string SoPhieu { get; set; }

        public DateTime? NgayNhap { get; set; }

        public int? MaKhoNhap { get; set; }

        public int? MaNCC { get; set; }

        [StringLength(100)]
        public string NguoiGiaoHang { get; set; }

        [StringLength(50)]
        public string SoHoaDon { get; set; }

        public DateOnly? NgayHoaDon { get; set; }

        public decimal? TongTien { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public int? NguoiLapPhieu { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }
        
        // Navigation properties
        public string TenKho { get; set; }
        public string TenNCC { get; set; }
        public string TenNguoiLapPhieu { get; set; }
    }

    public class CreatePhieuNhapKhoDTO
    {
        [Required(ErrorMessage = "Số phiếu không được để trống")]
        [StringLength(50, ErrorMessage = "Số phiếu không vượt quá 50 ký tự")]
        public string SoPhieu { get; set; }

        public DateTime? NgayNhap { get; set; }

        public int? MaKhoNhap { get; set; }

        public int? MaNCC { get; set; }

        [StringLength(100, ErrorMessage = "Người giao hàng không vượt quá 100 ký tự")]
        public string NguoiGiaoHang { get; set; }

        [StringLength(50, ErrorMessage = "Số hóa đơn không vượt quá 50 ký tự")]
        public string SoHoaDon { get; set; }

        public DateOnly? NgayHoaDon { get; set; }

        public decimal? TongTien { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không vượt quá 500 ký tự")]
        public string GhiChu { get; set; }

        [StringLength(50, ErrorMessage = "Trạng thái không vượt quá 50 ký tự")]
        public string TrangThai { get; set; } = "Chờ xác nhận";
    }

    public class UpdatePhieuNhapKhoDTO : CreatePhieuNhapKhoDTO
    {
        [Required]
        public int MaPhieuNhap { get; set; }
    }
}
