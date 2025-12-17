using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilcoManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVatTu_NguoiTaoUseNguoiDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__VatTu__NguoiTao__59FA5E80",
                table: "VatTu");

            migrationBuilder.AddColumn<int>(
                name: "NhanVienMaNV",
                table: "VatTu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VatTu_NhanVienMaNV",
                table: "VatTu",
                column: "NhanVienMaNV");

            migrationBuilder.AddForeignKey(
                name: "FK_VatTu_NhanVien_NhanVienMaNV",
                table: "VatTu",
                column: "NhanVienMaNV",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__VatTu__NguoiTao",
                table: "VatTu",
                column: "NguoiTao",
                principalTable: "NguoiDung",
                principalColumn: "MaND");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VatTu_NhanVien_NhanVienMaNV",
                table: "VatTu");

            migrationBuilder.DropForeignKey(
                name: "FK__VatTu__NguoiTao",
                table: "VatTu");

            migrationBuilder.DropIndex(
                name: "IX_VatTu_NhanVienMaNV",
                table: "VatTu");

            migrationBuilder.DropColumn(
                name: "NhanVienMaNV",
                table: "VatTu");

            migrationBuilder.AddForeignKey(
                name: "FK__VatTu__NguoiTao__59FA5E80",
                table: "VatTu",
                column: "NguoiTao",
                principalTable: "NhanVien",
                principalColumn: "MaNV");
        }
    }
}
