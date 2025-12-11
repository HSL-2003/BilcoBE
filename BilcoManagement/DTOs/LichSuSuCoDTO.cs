using System;
using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class LichSuSuCoDTO
    {
        public int MaSuCo { get; set; }

        public int? MaThietBi { get; set; }

        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string MucDo { get; set; }

        public DateTime? ThoiGianPhatHien { get; set; }

        public int? NguoiBaoCao { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(500)]
        public string GiaiPhap { get; set; }

        public DateTime? NgayXuLy { get; set; }
    }

    public class CreateLichSuSuCoDTO
    {
        public int? MaThietBi { get; set; }

        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string MucDo { get; set; }

        public DateTime? ThoiGianPhatHien { get; set; }

        public int? NguoiBaoCao { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(500)]
        public string GiaiPhap { get; set; }

        public DateTime? NgayXuLy { get; set; }
    }

    public class UpdateLichSuSuCoDTO : CreateLichSuSuCoDTO
    {
        [Required]
        public int MaSuCo { get; set; }
    }
}
