using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class ChiTietPhuTungDTO
    {
        public int MaChiTiet { get; set; }

        [Required]
        public int? MaPhieu { get; set; }

        [Required]
        [StringLength(200)]
        public string TenPhuTung { get; set; }

        [Range(0, int.MaxValue)]
        public int? SoLuong { get; set; }

        [StringLength(20)]
        public string DonViTinh { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }

    public class CreateChiTietPhuTungDTO
    {
        [Required]
        public int? MaPhieu { get; set; }

        [Required]
        [StringLength(200)]
        public string TenPhuTung { get; set; }

        [Range(0, int.MaxValue)]
        public int? SoLuong { get; set; }

        [StringLength(20)]
        public string DonViTinh { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }

    public class UpdateChiTietPhuTungDTO : CreateChiTietPhuTungDTO
    {
        [Required]
        public int MaChiTiet { get; set; }
    }
}
