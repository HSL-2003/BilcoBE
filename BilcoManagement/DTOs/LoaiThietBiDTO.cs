using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class LoaiThietBiDTO
    {
        public int MaLoai { get; set; }

        [Required]
        [StringLength(100)]
        public string TenLoai { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }
    }

    public class CreateLoaiThietBiDTO
    {
        [Required]
        [StringLength(100)]
        public string TenLoai { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }
    }

    public class UpdateLoaiThietBiDTO : CreateLoaiThietBiDTO
    {
        [Required]
        public int MaLoai { get; set; }
    }
}
