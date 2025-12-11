using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class ChiTietPhieuNhapDTO
    {
        public int MaCTPN { get; set; }

        [Required]
        public int? MaPhieuNhap { get; set; }

        [Required]
        public int? MaVT { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuong { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DonGia { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ThanhTien { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }

    public class CreateChiTietPhieuNhapDTO
    {
        [Required]
        public int? MaPhieuNhap { get; set; }

        [Required]
        public int? MaVT { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuong { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DonGia { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ThanhTien { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }

    public class UpdateChiTietPhieuNhapDTO : CreateChiTietPhieuNhapDTO
    {
        [Required]
        public int MaCTPN { get; set; }
    }
}
