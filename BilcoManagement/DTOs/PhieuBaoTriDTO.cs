using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class PhieuBaoTriDTO
    {
        public int MaPhieu { get; set; }

        public int? MaKeHoach { get; set; }

        public int? MaThietBi { get; set; }

        public int? NhanVienThucHien { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        [StringLength(500)]
        public string TinhTrangTruocBT { get; set; }

        [StringLength(500)]
        public string TinhTrangSauBT { get; set; }

        [StringLength(500)]
        public string KetQua { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public int? NguoiXacNhan { get; set; }

        public DateTime? NgayXacNhan { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }
        
        // Navigation properties
        public string TenKeHoach { get; set; }
        public string TenThietBi { get; set; }
        public string TenNhanVienThucHien { get; set; }
        public string TenNguoiXacNhan { get; set; }
    }

    public class CreatePhieuBaoTriDTO
    {
        public int? MaKeHoach { get; set; }

        public int? MaThietBi { get; set; }

        public int? NhanVienThucHien { get; set; }

        public DateTime? ThoiGianBatDau { get; set; }

        public DateTime? ThoiGianKetThuc { get; set; }

        [StringLength(500, ErrorMessage = "Tình trạng trước bảo trì không vượt quá 500 ký tự")]
        public string TinhTrangTruocBT { get; set; }

        [StringLength(500, ErrorMessage = "Tình trạng sau bảo trì không vượt quá 500 ký tự")]
        public string TinhTrangSauBT { get; set; }

        [StringLength(500, ErrorMessage = "Kết quả không vượt quá 500 ký tự")]
        public string KetQua { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không vượt quá 500 ký tự")]
        public string GhiChu { get; set; }

        [StringLength(50, ErrorMessage = "Trạng thái không vượt quá 50 ký tự")]
        public string TrangThai { get; set; } = "Đang thực hiện";
    }

    public class UpdatePhieuBaoTriDTO : CreatePhieuBaoTriDTO
    {
        [Required]
        public int MaPhieu { get; set; }

        public int? NguoiXacNhan { get; set; }

        public DateTime? NgayXacNhan { get; set; }
    }
}
