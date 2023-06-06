using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApiApp.Migrations
{
    public partial class AddDonHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    IdDonHang = table.Column<Guid>(type: "uuid", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NgayGiao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TinhTrangDonHang = table.Column<int>(type: "integer", nullable: false),
                    NguoiNhan = table.Column<string>(type: "text", nullable: true),
                    DiaChiGiao = table.Column<string>(type: "text", nullable: true),
                    SoDienThoai = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.IdDonHang);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    IdHangHoa = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDonHang = table.Column<Guid>(type: "uuid", nullable: false),
                    SoLuong = table.Column<int>(type: "integer", nullable: false),
                    DonGia = table.Column<double>(type: "double precision", nullable: false),
                    GiamGia = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => new { x.IdDonHang, x.IdHangHoa });
                    table.ForeignKey(
                        name: "FK_DonHangCT_DonHang",
                        column: x => x.IdDonHang,
                        principalTable: "DonHang",
                        principalColumn: "IdDonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangCT_HangHoa",
                        column: x => x.IdHangHoa,
                        principalTable: "HangHoa",
                        principalColumn: "IdHangHoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_IdHangHoa",
                table: "ChiTietDonHang",
                column: "IdHangHoa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "DonHang");
        }
    }
}
