using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilcoManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonViTinh",
                columns: table => new
                {
                    MaDVT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDVT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonViTin__3D895AFE9C8B0713", x => x.MaDVT);
                });

            migrationBuilder.CreateTable(
                name: "LoaiThietBi",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TieuChuanAnToan = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiThie__730A5759B88BB5B9", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "LoaiVatTu",
                columns: table => new
                {
                    MaLoaiVT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiVT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiVatT__1224E204BED8466A", x => x.MaLoaiVT);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNCC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNCC = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NguoiLienHe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhaCungC__3A185DEBE6AFAA11", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyen",
                columns: table => new
                {
                    MaQuyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhanQuye__1D4B7ED443CB0F65", x => x.MaQuyen);
                });

            migrationBuilder.CreateTable(
                name: "ThietBi",
                columns: table => new
                {
                    MaThietBi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThietBi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaLoai = table.Column<int>(type: "int", nullable: true),
                    MaSo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ViTriLapDat = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NgayLapDat = table.Column<DateOnly>(type: "date", nullable: true),
                    NgayHetHanBaoHanh = table.Column<DateOnly>(type: "date", nullable: true),
                    TinhTrang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Đang hoạt động"),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HinhAnh = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThietBi__8AEC71F793B7F867", x => x.MaThietBi);
                    table.ForeignKey(
                        name: "FK__ThietBi__MaLoai__3F466844",
                        column: x => x.MaLoai,
                        principalTable: "LoaiThietBi",
                        principalColumn: "MaLoai");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDieuChuyen",
                columns: table => new
                {
                    MaCTDC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDieuChuyen = table.Column<int>(type: "int", nullable: true),
                    MaVT = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__1E4E40F5A1140704", x => x.MaCTDC);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietKiemKe",
                columns: table => new
                {
                    MaCTKK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieuKK = table.Column<int>(type: "int", nullable: true),
                    MaVT = table.Column<int>(type: "int", nullable: true),
                    SoLuongTheoSoSach = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoLuongThucTe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChenhLech = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietK__1E4E09D676825D5C", x => x.MaCTKK);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuNhap",
                columns: table => new
                {
                    MaCTPN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieuNhap = table.Column<int>(type: "int", nullable: true),
                    MaVT = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietP__1E4E6075C7DF3DEE", x => x.MaCTPN);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuXuat",
                columns: table => new
                {
                    MaCTPX = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieuXuat = table.Column<int>(type: "int", nullable: true),
                    MaVT = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonGiaXuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietP__1E4E606F9E359980", x => x.MaCTPX);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhuTung",
                columns: table => new
                {
                    MaChiTiet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieu = table.Column<int>(type: "int", nullable: true),
                    TenPhuTung = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    DonViTinh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietP__CDF0A114EFF388F8", x => x.MaChiTiet);
                });

            migrationBuilder.CreateTable(
                name: "DieuChuyenKho",
                columns: table => new
                {
                    MaDieuChuyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoPhieu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NgayDieuChuyen = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MaKhoXuat = table.Column<int>(type: "int", nullable: true),
                    MaKhoNhan = table.Column<int>(type: "int", nullable: true),
                    LyDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NguoiLapPhieu = table.Column<int>(type: "int", nullable: true),
                    NguoiNhanHang = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Đã hoàn thành"),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DieuChuy__750B0A6D91FDC6DB", x => x.MaDieuChuyen);
                });

            migrationBuilder.CreateTable(
                name: "HinhAnhBaoTri",
                columns: table => new
                {
                    MaHinhAnh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieu = table.Column<int>(type: "int", nullable: true),
                    DuongDanHinhAnh = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    LoaiHinhAnh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HinhAnhB__A9C37A9B01F565F9", x => x.MaHinhAnh);
                });

            migrationBuilder.CreateTable(
                name: "KeHoachBaoTri",
                columns: table => new
                {
                    MaKeHoach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaThietBi = table.Column<int>(type: "int", nullable: true),
                    LoaiBaoTri = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChuKyBaoTri = table.Column<int>(type: "int", nullable: true),
                    NgayBatDau = table.Column<DateOnly>(type: "date", nullable: true),
                    NgayKetThuc = table.Column<DateOnly>(type: "date", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Chưa thực hiện"),
                    NguoiTao = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KeHoachB__88C5741FED66D971", x => x.MaKeHoach);
                    table.ForeignKey(
                        name: "FK__KeHoachBa__MaThi__4316F928",
                        column: x => x.MaThietBi,
                        principalTable: "ThietBi",
                        principalColumn: "MaThietBi");
                });

            migrationBuilder.CreateTable(
                name: "Kho",
                columns: table => new
                {
                    MaKho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NguoiQuanLy = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Kho__3BDA9350F8F4FAC4", x => x.MaKho);
                });

            migrationBuilder.CreateTable(
                name: "KiemKeKho",
                columns: table => new
                {
                    MaPhieuKK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoPhieu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NgayKiemKe = table.Column<DateOnly>(type: "date", nullable: false),
                    MaKho = table.Column<int>(type: "int", nullable: true),
                    NguoiKiemKe = table.Column<int>(type: "int", nullable: true),
                    LyDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Đang kiểm kê"),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KiemKeKh__880EE8E856CE8FF5", x => x.MaPhieuKK);
                    table.ForeignKey(
                        name: "FK__KiemKeKho__MaKho__160F4887",
                        column: x => x.MaKho,
                        principalTable: "Kho",
                        principalColumn: "MaKho");
                });

            migrationBuilder.CreateTable(
                name: "LichSuSuCo",
                columns: table => new
                {
                    MaSuCo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaThietBi = table.Column<int>(type: "int", nullable: true),
                    TieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MucDo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThoiGianPhatHien = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiBaoCao = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Chưa xử lý"),
                    GiaiPhap = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NgayXuLy = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LichSuSu__A69DF79FCA6E092E", x => x.MaSuCo);
                    table.ForeignKey(
                        name: "FK__LichSuSuC__MaThi__1EA48E88",
                        column: x => x.MaThietBi,
                        principalTable: "ThietBi",
                        principalColumn: "MaThietBi");
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaND = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNV = table.Column<int>(type: "int", nullable: true),
                    TenDangNhap = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    MaQuyen = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PhongBan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChucVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    LastLogin = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NguoiDun__2725D724AAEA6ADE", x => x.MaND);
                    table.ForeignKey(
                        name: "FK__NguoiDung__MaQuy__6C190EBB",
                        column: x => x.MaQuyen,
                        principalTable: "PhanQuyen",
                        principalColumn: "MaQuyen");
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__2725D70AA3601EA0", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK_NhanVien_NguoiDung",
                        column: x => x.UserID,
                        principalTable: "NguoiDung",
                        principalColumn: "MaND");
                });

            migrationBuilder.CreateTable(
                name: "PhieuBaoTri",
                columns: table => new
                {
                    MaPhieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKeHoach = table.Column<int>(type: "int", nullable: true),
                    MaThietBi = table.Column<int>(type: "int", nullable: true),
                    NhanVienThucHien = table.Column<int>(type: "int", nullable: true),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime", nullable: true),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime", nullable: true),
                    TinhTrangTruocBT = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TinhTrangSauBT = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    KetQua = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NguoiXacNhan = table.Column<int>(type: "int", nullable: true),
                    NgayXacNhan = table.Column<DateTime>(type: "datetime", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Chờ xác nhận")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhieuBao__2660BFE02A7774C5", x => x.MaPhieu);
                    table.ForeignKey(
                        name: "FK__PhieuBaoT__MaKeH__48CFD27E",
                        column: x => x.MaKeHoach,
                        principalTable: "KeHoachBaoTri",
                        principalColumn: "MaKeHoach");
                    table.ForeignKey(
                        name: "FK__PhieuBaoT__MaThi__49C3F6B7",
                        column: x => x.MaThietBi,
                        principalTable: "ThietBi",
                        principalColumn: "MaThietBi");
                    table.ForeignKey(
                        name: "FK__PhieuBaoT__Nguoi__4BAC3F29",
                        column: x => x.NguoiXacNhan,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                    table.ForeignKey(
                        name: "FK__PhieuBaoT__NhanV__4AB81AF0",
                        column: x => x.NhanVienThucHien,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhapKho",
                columns: table => new
                {
                    MaPhieuNhap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoPhieu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MaKhoNhap = table.Column<int>(type: "int", nullable: true),
                    MaNCC = table.Column<int>(type: "int", nullable: true),
                    NguoiGiaoHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoHoaDon = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NgayHoaDon = table.Column<DateOnly>(type: "date", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NguoiLapPhieu = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Đã hoàn thành")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhieuNha__1470EF3B2B4F1850", x => x.MaPhieuNhap);
                    table.ForeignKey(
                        name: "FK__PhieuNhap__MaKho__72C60C4A",
                        column: x => x.MaKhoNhap,
                        principalTable: "Kho",
                        principalColumn: "MaKho");
                    table.ForeignKey(
                        name: "FK__PhieuNhap__MaNCC__73BA3083",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC");
                    table.ForeignKey(
                        name: "FK__PhieuNhap__Nguoi__75A278F5",
                        column: x => x.NguoiLapPhieu,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateTable(
                name: "VatTu",
                columns: table => new
                {
                    MaVT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLoaiVT = table.Column<int>(type: "int", nullable: true),
                    MaDVT = table.Column<int>(type: "int", nullable: true),
                    TenVT = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaVach = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MaNCC = table.Column<int>(type: "int", nullable: true),
                    ThoiGianBaoHanh = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HinhAnh = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    NguoiTao = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VatTu__2725103E8899EE53", x => x.MaVT);
                    table.ForeignKey(
                        name: "FK__VatTu__MaDVT__571DF1D5",
                        column: x => x.MaDVT,
                        principalTable: "DonViTinh",
                        principalColumn: "MaDVT");
                    table.ForeignKey(
                        name: "FK__VatTu__MaLoaiVT__5629CD9C",
                        column: x => x.MaLoaiVT,
                        principalTable: "LoaiVatTu",
                        principalColumn: "MaLoaiVT");
                    table.ForeignKey(
                        name: "FK__VatTu__MaNCC__5812160E",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC");
                    table.ForeignKey(
                        name: "FK__VatTu__NguoiTao__59FA5E80",
                        column: x => x.NguoiTao,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateTable(
                name: "PhieuXuatKho",
                columns: table => new
                {
                    MaPhieuXuat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoPhieu = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NgayXuat = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MaKhoXuat = table.Column<int>(type: "int", nullable: true),
                    MaPhieuBaoTri = table.Column<int>(type: "int", nullable: true),
                    LyDoXuat = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NguoiNhanHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NguoiLapPhieu = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Đã hoàn thành"),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhieuXua__26C4B5A2FE7B5A65", x => x.MaPhieuXuat);
                    table.ForeignKey(
                        name: "FK__PhieuXuat__MaKho__7F2BE32F",
                        column: x => x.MaKhoXuat,
                        principalTable: "Kho",
                        principalColumn: "MaKho");
                    table.ForeignKey(
                        name: "FK__PhieuXuat__MaPhi__00200768",
                        column: x => x.MaPhieuBaoTri,
                        principalTable: "PhieuBaoTri",
                        principalColumn: "MaPhieu");
                    table.ForeignKey(
                        name: "FK__PhieuXuat__Nguoi__01142BA1",
                        column: x => x.NguoiLapPhieu,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateTable(
                name: "TonKho",
                columns: table => new
                {
                    MaTonKho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKho = table.Column<int>(type: "int", nullable: true),
                    MaVT = table.Column<int>(type: "int", nullable: true),
                    SoLuongTon = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m),
                    SoLuongKhaDung = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m),
                    SoLuongToiThieu = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m),
                    ViTriLuuTru = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TonKho__2D970FAAD1B1B4E2", x => x.MaTonKho);
                    table.ForeignKey(
                        name: "FK__TonKho__MaKho__60A75C0F",
                        column: x => x.MaKho,
                        principalTable: "Kho",
                        principalColumn: "MaKho");
                    table.ForeignKey(
                        name: "FK__TonKho__MaVT__619B8048",
                        column: x => x.MaVT,
                        principalTable: "VatTu",
                        principalColumn: "MaVT");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDieuChuyen_MaDieuChuyen",
                table: "ChiTietDieuChuyen",
                column: "MaDieuChuyen");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDieuChuyen_MaVT",
                table: "ChiTietDieuChuyen",
                column: "MaVT");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKiemKe_MaPhieuKK",
                table: "ChiTietKiemKe",
                column: "MaPhieuKK");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKiemKe_MaVT",
                table: "ChiTietKiemKe",
                column: "MaVT");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhap_MaPhieuNhap",
                table: "ChiTietPhieuNhap",
                column: "MaPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhap_MaVT",
                table: "ChiTietPhieuNhap",
                column: "MaVT");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuXuat_MaPhieuXuat",
                table: "ChiTietPhieuXuat",
                column: "MaPhieuXuat");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuXuat_MaVT",
                table: "ChiTietPhieuXuat",
                column: "MaVT");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhuTung_MaPhieu",
                table: "ChiTietPhuTung",
                column: "MaPhieu");

            migrationBuilder.CreateIndex(
                name: "IX_DieuChuyenKho_MaKhoNhan",
                table: "DieuChuyenKho",
                column: "MaKhoNhan");

            migrationBuilder.CreateIndex(
                name: "IX_DieuChuyenKho_MaKhoXuat",
                table: "DieuChuyenKho",
                column: "MaKhoXuat");

            migrationBuilder.CreateIndex(
                name: "IX_DieuChuyenKho_NguoiLapPhieu",
                table: "DieuChuyenKho",
                column: "NguoiLapPhieu");

            migrationBuilder.CreateIndex(
                name: "IX_DieuChuyenKho_NguoiNhanHang",
                table: "DieuChuyenKho",
                column: "NguoiNhanHang");

            migrationBuilder.CreateIndex(
                name: "UQ__DieuChuy__960AAEE37F664455",
                table: "DieuChuyenKho",
                column: "SoPhieu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnhBaoTri_MaPhieu",
                table: "HinhAnhBaoTri",
                column: "MaPhieu");

            migrationBuilder.CreateIndex(
                name: "IX_KeHoachBaoTri_MaThietBi",
                table: "KeHoachBaoTri",
                column: "MaThietBi");

            migrationBuilder.CreateIndex(
                name: "IX_KeHoachBaoTri_NguoiTao",
                table: "KeHoachBaoTri",
                column: "NguoiTao");

            migrationBuilder.CreateIndex(
                name: "IX_Kho_NguoiQuanLy",
                table: "Kho",
                column: "NguoiQuanLy");

            migrationBuilder.CreateIndex(
                name: "IX_KiemKeKho_MaKho",
                table: "KiemKeKho",
                column: "MaKho");

            migrationBuilder.CreateIndex(
                name: "IX_KiemKeKho_NguoiKiemKe",
                table: "KiemKeKho",
                column: "NguoiKiemKe");

            migrationBuilder.CreateIndex(
                name: "UQ__KiemKeKh__960AAEE3724913EE",
                table: "KiemKeKho",
                column: "SoPhieu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichSuSuCo_MaThietBi",
                table: "LichSuSuCo",
                column: "MaThietBi");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuSuCo_NguoiBaoCao",
                table: "LichSuSuCo",
                column: "NguoiBaoCao");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaNV",
                table: "NguoiDung",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaQuyen",
                table: "NguoiDung",
                column: "MaQuyen");

            migrationBuilder.CreateIndex(
                name: "UQ__NguoiDun__55F68FC076A218F8",
                table: "NguoiDung",
                column: "TenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_UserID",
                table: "NhanVien",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__A9D105344A132BAD",
                table: "NhanVien",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuBaoTri_MaKeHoach",
                table: "PhieuBaoTri",
                column: "MaKeHoach");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuBaoTri_MaThietBi",
                table: "PhieuBaoTri",
                column: "MaThietBi");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuBaoTri_NguoiXacNhan",
                table: "PhieuBaoTri",
                column: "NguoiXacNhan");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuBaoTri_NhanVienThucHien",
                table: "PhieuBaoTri",
                column: "NhanVienThucHien");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhapKho_MaKhoNhap",
                table: "PhieuNhapKho",
                column: "MaKhoNhap");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhapKho_MaNCC",
                table: "PhieuNhapKho",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhapKho_NguoiLapPhieu",
                table: "PhieuNhapKho",
                column: "NguoiLapPhieu");

            migrationBuilder.CreateIndex(
                name: "UQ__PhieuNha__960AAEE3AF547A17",
                table: "PhieuNhapKho",
                column: "SoPhieu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuatKho_MaKhoXuat",
                table: "PhieuXuatKho",
                column: "MaKhoXuat");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuatKho_MaPhieuBaoTri",
                table: "PhieuXuatKho",
                column: "MaPhieuBaoTri");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuatKho_NguoiLapPhieu",
                table: "PhieuXuatKho",
                column: "NguoiLapPhieu");

            migrationBuilder.CreateIndex(
                name: "UQ__PhieuXua__960AAEE32DB10CF7",
                table: "PhieuXuatKho",
                column: "SoPhieu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThietBi_MaLoai",
                table: "ThietBi",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "UQ__ThietBi__2725087C40D5B15D",
                table: "ThietBi",
                column: "MaSo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TonKho_MaVT",
                table: "TonKho",
                column: "MaVT");

            migrationBuilder.CreateIndex(
                name: "UQ__TonKho__C9A8C252169D38F0",
                table: "TonKho",
                columns: new[] { "MaKho", "MaVT" },
                unique: true,
                filter: "[MaKho] IS NOT NULL AND [MaVT] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VatTu_MaDVT",
                table: "VatTu",
                column: "MaDVT");

            migrationBuilder.CreateIndex(
                name: "IX_VatTu_MaLoaiVT",
                table: "VatTu",
                column: "MaLoaiVT");

            migrationBuilder.CreateIndex(
                name: "IX_VatTu_MaNCC",
                table: "VatTu",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_VatTu_NguoiTao",
                table: "VatTu",
                column: "NguoiTao");

            migrationBuilder.CreateIndex(
                name: "UQ__VatTu__8BBF4A1C9A1AEDA5",
                table: "VatTu",
                column: "MaVach",
                unique: true,
                filter: "[MaVach] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDi__MaDie__114A936A",
                table: "ChiTietDieuChuyen",
                column: "MaDieuChuyen",
                principalTable: "DieuChuyenKho",
                principalColumn: "MaDieuChuyen");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDie__MaVT__123EB7A3",
                table: "ChiTietDieuChuyen",
                column: "MaVT",
                principalTable: "VatTu",
                principalColumn: "MaVT");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietKi__MaPhi__1AD3FDA4",
                table: "ChiTietKiemKe",
                column: "MaPhieuKK",
                principalTable: "KiemKeKho",
                principalColumn: "MaPhieuKK");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietKie__MaVT__1BC821DD",
                table: "ChiTietKiemKe",
                column: "MaVT",
                principalTable: "VatTu",
                principalColumn: "MaVT");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietPh__MaPhi__797309D9",
                table: "ChiTietPhieuNhap",
                column: "MaPhieuNhap",
                principalTable: "PhieuNhapKho",
                principalColumn: "MaPhieuNhap");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietPhi__MaVT__7A672E12",
                table: "ChiTietPhieuNhap",
                column: "MaVT",
                principalTable: "VatTu",
                principalColumn: "MaVT");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietPh__MaPhi__04E4BC85",
                table: "ChiTietPhieuXuat",
                column: "MaPhieuXuat",
                principalTable: "PhieuXuatKho",
                principalColumn: "MaPhieuXuat");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietPhi__MaVT__05D8E0BE",
                table: "ChiTietPhieuXuat",
                column: "MaVT",
                principalTable: "VatTu",
                principalColumn: "MaVT");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietPh__MaPhi__236943A5",
                table: "ChiTietPhuTung",
                column: "MaPhieu",
                principalTable: "PhieuBaoTri",
                principalColumn: "MaPhieu");

            migrationBuilder.AddForeignKey(
                name: "FK__DieuChuye__MaKho__0A9D95DB",
                table: "DieuChuyenKho",
                column: "MaKhoXuat",
                principalTable: "Kho",
                principalColumn: "MaKho");

            migrationBuilder.AddForeignKey(
                name: "FK__DieuChuye__MaKho__0B91BA14",
                table: "DieuChuyenKho",
                column: "MaKhoNhan",
                principalTable: "Kho",
                principalColumn: "MaKho");

            migrationBuilder.AddForeignKey(
                name: "FK__DieuChuye__Nguoi__0C85DE4D",
                table: "DieuChuyenKho",
                column: "NguoiLapPhieu",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__DieuChuye__Nguoi__0D7A0286",
                table: "DieuChuyenKho",
                column: "NguoiNhanHang",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__HinhAnhBa__MaPhi__2739D489",
                table: "HinhAnhBaoTri",
                column: "MaPhieu",
                principalTable: "PhieuBaoTri",
                principalColumn: "MaPhieu");

            migrationBuilder.AddForeignKey(
                name: "FK__KeHoachBa__Nguoi__44FF419A",
                table: "KeHoachBaoTri",
                column: "NguoiTao",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__Kho__NguoiQuanLy__5CD6CB2B",
                table: "Kho",
                column: "NguoiQuanLy",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__KiemKeKho__Nguoi__17036CC0",
                table: "KiemKeKho",
                column: "NguoiKiemKe",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__LichSuSuC__Nguoi__1F98B2C1",
                table: "LichSuSuCo",
                column: "NguoiBaoCao",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__NguoiDung__MaNV__6B24EA82",
                table: "NguoiDung",
                column: "MaNV",
                principalTable: "NhanVien",
                principalColumn: "MaNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__NguoiDung__MaNV__6B24EA82",
                table: "NguoiDung");

            migrationBuilder.DropTable(
                name: "ChiTietDieuChuyen");

            migrationBuilder.DropTable(
                name: "ChiTietKiemKe");

            migrationBuilder.DropTable(
                name: "ChiTietPhieuNhap");

            migrationBuilder.DropTable(
                name: "ChiTietPhieuXuat");

            migrationBuilder.DropTable(
                name: "ChiTietPhuTung");

            migrationBuilder.DropTable(
                name: "HinhAnhBaoTri");

            migrationBuilder.DropTable(
                name: "LichSuSuCo");

            migrationBuilder.DropTable(
                name: "TonKho");

            migrationBuilder.DropTable(
                name: "DieuChuyenKho");

            migrationBuilder.DropTable(
                name: "KiemKeKho");

            migrationBuilder.DropTable(
                name: "PhieuNhapKho");

            migrationBuilder.DropTable(
                name: "PhieuXuatKho");

            migrationBuilder.DropTable(
                name: "VatTu");

            migrationBuilder.DropTable(
                name: "Kho");

            migrationBuilder.DropTable(
                name: "PhieuBaoTri");

            migrationBuilder.DropTable(
                name: "DonViTinh");

            migrationBuilder.DropTable(
                name: "LoaiVatTu");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "KeHoachBaoTri");

            migrationBuilder.DropTable(
                name: "ThietBi");

            migrationBuilder.DropTable(
                name: "LoaiThietBi");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "PhanQuyen");
        }
    }
}
