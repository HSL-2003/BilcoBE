namespace BilcoManagement.DTOs
{
    public class ThietBiDTO
    {
        public int MaThietBi { get; set; }

        public string TenThietBi { get; set; }

        public int? MaLoai { get; set; }

        public string MaSo { get; set; }

        public string ViTriLapDat { get; set; }

        public DateOnly? NgayLapDat { get; set; }

        public DateOnly? NgayHetHanBaoHanh { get; set; }

        public string TinhTrang { get; set; }

        public string GhiChu { get; set; }

        public string HinhAnh { get; set; }

    }
    public class CreateThietBiDTO
    {
        public string TenThietBi { get; set; }

        public int? MaLoai { get; set; }

        public string MaSo { get; set; }

       

        public DateOnly? NgayLapDat { get; set; }

        public DateOnly? NgayHetHanBaoHanh { get; set; }

        public string TinhTrang { get; set; }

        public string GhiChu { get; set; }

        public string HinhAnh { get; set; }

    }
    public class UpdateThietBiDTO : CreateThietBiDTO
    {
        public int MaThietBi { get; set; }
    }
}
