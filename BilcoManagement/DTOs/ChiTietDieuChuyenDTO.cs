using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class ChiTietDieuChuyenDTO
    {
        public int MaCTDC { get; set; }

        [Required]
        public int? MaDieuChuyen { get; set; }

        [Required]
        public int? MaVT { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuong { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }

    public class CreateChiTietDieuChuyenDTO
    {
        [Required]
        public int? MaDieuChuyen { get; set; }

        [Required]
        public int? MaVT { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SoLuong { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }

    public class UpdateChiTietDieuChuyenDTO : CreateChiTietDieuChuyenDTO
    {
        [Required]
        public int MaCTDC { get; set; }
    }
}
