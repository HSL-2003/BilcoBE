using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilcoManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLichSuSuCo_UseNguoiDungInsteadOfNhanVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__LichSuSuC__Nguoi__1F98B2C1",
                table: "LichSuSuCo");

            migrationBuilder.RenameColumn(
                name: "NguoiBaoCao",
                table: "LichSuSuCo",
                newName: "NhanVienMaNV");

            migrationBuilder.RenameIndex(
                name: "IX_LichSuSuCo_NguoiBaoCao",
                table: "LichSuSuCo",
                newName: "IX_LichSuSuCo_NhanVienMaNV");

            migrationBuilder.AddColumn<int>(
                name: "NguoiDungID",
                table: "LichSuSuCo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichSuSuCo_NguoiDungID",
                table: "LichSuSuCo",
                column: "NguoiDungID");

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuSuCo_NhanVien_NhanVienMaNV",
                table: "LichSuSuCo",
                column: "NhanVienMaNV",
                principalTable: "NhanVien",
                principalColumn: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK__LichSuSuC__NguoiDungID",
                table: "LichSuSuCo",
                column: "NguoiDungID",
                principalTable: "NguoiDung",
                principalColumn: "MaND");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichSuSuCo_NhanVien_NhanVienMaNV",
                table: "LichSuSuCo");

            migrationBuilder.DropForeignKey(
                name: "FK__LichSuSuC__NguoiDungID",
                table: "LichSuSuCo");

            migrationBuilder.DropIndex(
                name: "IX_LichSuSuCo_NguoiDungID",
                table: "LichSuSuCo");

            migrationBuilder.DropColumn(
                name: "NguoiDungID",
                table: "LichSuSuCo");

            migrationBuilder.RenameColumn(
                name: "NhanVienMaNV",
                table: "LichSuSuCo",
                newName: "NguoiBaoCao");

            migrationBuilder.RenameIndex(
                name: "IX_LichSuSuCo_NhanVienMaNV",
                table: "LichSuSuCo",
                newName: "IX_LichSuSuCo_NguoiBaoCao");

            migrationBuilder.AddForeignKey(
                name: "FK__LichSuSuC__Nguoi__1F98B2C1",
                table: "LichSuSuCo",
                column: "NguoiBaoCao",
                principalTable: "NhanVien",
                principalColumn: "MaNV");
        }
    }
}
