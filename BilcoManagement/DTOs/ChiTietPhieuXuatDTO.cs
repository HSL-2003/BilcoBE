using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class ChiTietPhieuXuatDTO
    {
        public int MaCTPX { get; set; }

        [Required]
        public int? MaPhieuXuat { get; set; }

        [Required]
        public int? MaVT { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuong { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DonGiaXuat { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ThanhTien { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }

    public class CreateChiTietPhieuXuatDTO
    {
        [Required]
        public int? MaPhieuXuat { get; set; }

        [Required]
        public int? MaVT { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuong { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DonGiaXuat { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ThanhTien { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }

    public class UpdateChiTietPhieuXuatDTO : CreateChiTietPhieuXuatDTO
    {
        [Required]
        public int MaCTPX { get; set; }
    }
}
