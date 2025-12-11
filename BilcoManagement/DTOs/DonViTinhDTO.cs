using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class DonViTinhDTO
    {
        public int MaDVT { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDVT { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }
    }

    public class CreateDonViTinhDTO
    {
        [Required]
        [StringLength(100)]
        public string TenDVT { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }
    }

    public class UpdateDonViTinhDTO : CreateDonViTinhDTO
    {
        [Required]
        public int MaDVT { get; set; }
    }
}
