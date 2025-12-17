using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilcoManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKho_UseNguoiDungInsteadOfNhanVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Kho__NguoiQuanLy__5CD6CB2B",
                table: "Kho");

            migrationBuilder.RenameColumn(
                name: "NguoiQuanLy",
                table: "Kho",
                newName: "NhanVienMaNV");

            migrationBuilder.RenameIndex(
                name: "IX_Kho_NguoiQuanLy",
                table: "Kho",
                newName: "IX_Kho_NhanVienMaNV");

            migrationBuilder.AddColumn<int>(
                name: "NguoiQuanLyID",
                table: "Kho",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kho_NguoiQuanLyID",
                table: "Kho",
                column: "NguoiQuanLyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kho_NhanVien_NhanVienMaNV",
                table: "Kho",
                column: "NhanVienMaNV",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__Kho__NguoiQuanLyID",
                table: "Kho",
                column: "NguoiQuanLyID",
                principalTable: "NguoiDung",
                principalColumn: "MaND");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kho_NhanVien_NhanVienMaNV",
                table: "Kho");

            migrationBuilder.DropForeignKey(
                name: "FK__Kho__NguoiQuanLyID",
                table: "Kho");

            migrationBuilder.DropIndex(
                name: "IX_Kho_NguoiQuanLyID",
                table: "Kho");

            migrationBuilder.DropColumn(
                name: "NguoiQuanLyID",
                table: "Kho");

            migrationBuilder.RenameColumn(
                name: "NhanVienMaNV",
                table: "Kho",
                newName: "NguoiQuanLy");

            migrationBuilder.RenameIndex(
                name: "IX_Kho_NhanVienMaNV",
                table: "Kho",
                newName: "IX_Kho_NguoiQuanLy");

            migrationBuilder.AddForeignKey(
                name: "FK__Kho__NguoiQuanLy__5CD6CB2B",
                table: "Kho",
                column: "NguoiQuanLy",
                principalTable: "NhanVien",
                principalColumn: "MaNV");
        }
    }
}
