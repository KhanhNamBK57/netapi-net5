using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApiApp.Migrations
{
    public partial class DbPortgreSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    IdLoai = table.Column<Guid>(type: "uuid", nullable: false),
                    TenLoai = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.IdLoai);
                });

            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    IdHangHoa = table.Column<Guid>(type: "uuid", nullable: false),
                    TenHangHoa = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    DonGia = table.Column<double>(type: "double precision", nullable: false),
                    GiamGia = table.Column<byte>(type: "smallint", nullable: false),
                    IdLoai = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.IdHangHoa);
                    table.ForeignKey(
                        name: "FK_HangHoa_Loai_IdLoai",
                        column: x => x.IdLoai,
                        principalTable: "Loai",
                        principalColumn: "IdLoai",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_IdLoai",
                table: "HangHoa",
                column: "IdLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HangHoa");

            migrationBuilder.DropTable(
                name: "Loai");
        }
    }
}
