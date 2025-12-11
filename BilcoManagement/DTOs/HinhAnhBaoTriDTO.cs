using System;
using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class HinhAnhBaoTriDTO
    {
        public int MaHinhAnh { get; set; }

        [Required]
        public int? MaPhieu { get; set; }

        [Required]
        [StringLength(500)]
        public string DuongDanHinhAnh { get; set; }

        [StringLength(100)]
        public string LoaiHinhAnh { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        public DateTime? NgayTao { get; set; }
    }

    public class CreateHinhAnhBaoTriDTO
    {
        [Required]
        public int? MaPhieu { get; set; }

        [Required]
        [StringLength(500)]
        public string DuongDanHinhAnh { get; set; }

        [StringLength(100)]
        public string LoaiHinhAnh { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        public DateTime? NgayTao { get; set; }
    }

    public class UpdateHinhAnhBaoTriDTO : CreateHinhAnhBaoTriDTO
    {
        [Required]
        public int MaHinhAnh { get; set; }
    }
}
