using System;
using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class DieuChuyenKhoDTO
    {
        public int MaDieuChuyen { get; set; }

        [Required]
        [StringLength(50)]
        public string SoPhieu { get; set; }

        public DateTime? NgayDieuChuyen { get; set; }

        [Required]
        public int? MaKhoXuat { get; set; }

        [Required]
        public int? MaKhoNhan { get; set; }

        [StringLength(500)]
        public string LyDo { get; set; }

        public int? NguoiLapPhieu { get; set; }

        public int? NguoiNhanHang { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
    }

    public class CreateDieuChuyenKhoDTO
    {
        [Required]
        [StringLength(50)]
        public string SoPhieu { get; set; }

        public DateTime? NgayDieuChuyen { get; set; }

        [Required]
        public int? MaKhoXuat { get; set; }

        [Required]
        public int? MaKhoNhan { get; set; }

        [StringLength(500)]
        public string LyDo { get; set; }

        public int? NguoiLapPhieu { get; set; }

        public int? NguoiNhanHang { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
    }

    public class UpdateDieuChuyenKhoDTO : CreateDieuChuyenKhoDTO
    {
        [Required]
        public int MaDieuChuyen { get; set; }
    }
}
