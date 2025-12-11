using System;
using System.ComponentModel.DataAnnotations;

namespace BilcoManagement.DTOs
{
    public class TonKhoDTO
    {
        public int MaTonKho { get; set; }

        [Required]
        public int? MaKho { get; set; }

        [Required]
        public int? MaVT { get; set; }

        public decimal? SoLuongTon { get; set; }

        public decimal? SoLuongKhaDung { get; set; }

        public decimal? SoLuongToiThieu { get; set; }

        [StringLength(200)]
        public string ViTriLuuTru { get; set; }

        public DateTime? NgayCapNhat { get; set; }
    }

    public class CreateTonKhoDTO
    {
        [Required]
        public int? MaKho { get; set; }

        [Required]
        public int? MaVT { get; set; }

        public decimal? SoLuongTon { get; set; }

        public decimal? SoLuongKhaDung { get; set; }

        public decimal? SoLuongToiThieu { get; set; }

        [StringLength(200)]
        public string ViTriLuuTru { get; set; }

        public DateTime? NgayCapNhat { get; set; }
    }

    public class UpdateTonKhoDTO : CreateTonKhoDTO
    {
        [Required]
        public int MaTonKho { get; set; }
    }
}
