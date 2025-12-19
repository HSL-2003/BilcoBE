using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilcoManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKeHoachBaoTri_NguoiTaoUseNguoiDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__KeHoachBa__Nguoi__44FF419A",
                table: "KeHoachBaoTri");

            migrationBuilder.AddColumn<int>(
                name: "NhanVienMaNV",
                table: "KeHoachBaoTri",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeHoachBaoTri_NhanVienMaNV",
                table: "KeHoachBaoTri",
                column: "NhanVienMaNV");

            migrationBuilder.AddForeignKey(
                name: "FK_KeHoachBaoTri_NhanVien_NhanVienMaNV",
                table: "KeHoachBaoTri",
                column: "NhanVienMaNV",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__KeHoachBa__Nguoi__NguoiDung",
                table: "KeHoachBaoTri",
                column: "NguoiTao",
                principalTable: "NguoiDung",
                principalColumn: "MaND");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeHoachBaoTri_NhanVien_NhanVienMaNV",
                table: "KeHoachBaoTri");

            migrationBuilder.DropForeignKey(
                name: "FK__KeHoachBa__Nguoi__NguoiDung",
                table: "KeHoachBaoTri");

            migrationBuilder.DropIndex(
                name: "IX_KeHoachBaoTri_NhanVienMaNV",
                table: "KeHoachBaoTri");

            migrationBuilder.DropColumn(
                name: "NhanVienMaNV",
                table: "KeHoachBaoTri");

            migrationBuilder.AddForeignKey(
                name: "FK__KeHoachBa__Nguoi__44FF419A",
                table: "KeHoachBaoTri",
                column: "NguoiTao",
                principalTable: "NhanVien",
                principalColumn: "MaNV");
        }
    }
}
