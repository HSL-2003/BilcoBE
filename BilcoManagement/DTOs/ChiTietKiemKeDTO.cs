using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class ChiTietKiemKeDTO
    {
        public int MaCTKK { get; set; }

        [Required]
        public int? MaPhieuKK { get; set; }

        [Required]
        public int? MaVT { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuongTheoSoSach { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuongThucTe { get; set; }

        public decimal ChenhLech { get; set; }

        [StringLength(500)]
        public string LyDo { get; set; }
    }

    public class CreateChiTietKiemKeDTO
    {
        [Required]
        public int? MaPhieuKK { get; set; }

        [Required]
        public int? MaVT { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuongTheoSoSach { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuongThucTe { get; set; }

        public decimal ChenhLech { get; set; }

        [StringLength(500)]
        public string LyDo { get; set; }
    }

    public class UpdateChiTietKiemKeDTO : CreateChiTietKiemKeDTO
    {
        [Required]
        public int MaCTKK { get; set; }
    }
}
