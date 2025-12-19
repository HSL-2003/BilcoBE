using System;
using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class KeHoachBaoTriDTO
    {
        public int MaKeHoach { get; set; }

        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; }

        public int? MaThietBi { get; set; }

        [StringLength(100)]
        public string LoaiBaoTri { get; set; }

        public int? ChuKyBaoTri { get; set; }

        public DateOnly? NgayBatDau { get; set; }

        public DateOnly? NgayKetThuc { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public int? NguoiTao { get; set; }

        public DateTime? NgayTao { get; set; }
        
        // Navigation properties
        public string TenNguoiTao { get; set; }
        public string TenThietBi { get; set; }
    }

    public class CreateKeHoachBaoTriDTO
    {
        [Required]
        [StringLength(200)]
        public string TieuDe { get; set; }

        public int? MaThietBi { get; set; }

        [StringLength(100)]
        public string LoaiBaoTri { get; set; }

        public int? ChuKyBaoTri { get; set; }

        public DateOnly? NgayBatDau { get; set; }

        public DateOnly? NgayKetThuc { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public int? NguoiTao { get; set; }
    }

    public class UpdateKeHoachBaoTriDTO : CreateKeHoachBaoTriDTO
    {
        [Required]
        public int MaKeHoach { get; set; }
    }
}
