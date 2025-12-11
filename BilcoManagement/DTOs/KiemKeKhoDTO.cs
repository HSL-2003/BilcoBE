using System;
using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class KiemKeKhoDTO
    {
        public int MaPhieuKK { get; set; }

        [Required]
        [StringLength(50)]
        public string SoPhieu { get; set; }

        public DateTime NgayKiemKe { get; set; }

        [Required]
        public int? MaKho { get; set; }

        public int? NguoiKiemKe { get; set; }

        [StringLength(500)]
        public string LyDo { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
    }

    public class CreateKiemKeKhoDTO
    {
        [Required]
        [StringLength(50)]
        public string SoPhieu { get; set; }

        public DateTime NgayKiemKe { get; set; } = DateTime.UtcNow;

        [Required]
        public int? MaKho { get; set; }

        public int? NguoiKiemKe { get; set; }

        [StringLength(500)]
        public string LyDo { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
    }

    public class UpdateKiemKeKhoDTO : CreateKiemKeKhoDTO
    {
        [Required]
        public int MaPhieuKK { get; set; }
    }
}
