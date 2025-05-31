using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseModels.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiGhe",
                columns: table => new
                {
                    MaLoaiGhe = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenLoaiGhe = table.Column<string>(type: "text", nullable: false),
                    GiaTien = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiGhe", x => x.MaLoaiGhe);
                });

            migrationBuilder.CreateTable(
                name: "LoaiHinh",
                columns: table => new
                {
                    MaLoaiHinh = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenLoaiHinh = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHinh", x => x.MaLoaiHinh);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNguoiDung = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoTen = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    MatKhauBam = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "Phim",
                columns: table => new
                {
                    MaPhim = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenPhim = table.Column<string>(type: "text", nullable: false),
                    DoDai = table.Column<long>(type: "bigint", nullable: false),
                    DanhGia = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phim", x => x.MaPhim);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    MaTheLoai = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenTheLoai = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.MaTheLoai);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHoaDon = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ThoiGianDat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaNguoiDung = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDon_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaChieu",
                columns: table => new
                {
                    MaCaChieu = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ThoiGianChieu = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SoHang = table.Column<long>(type: "bigint", nullable: false),
                    SoCot = table.Column<long>(type: "bigint", nullable: false),
                    MaPhim = table.Column<long>(type: "bigint", nullable: false),
                    MaLoaiHinh = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaChieu", x => x.MaCaChieu);
                    table.ForeignKey(
                        name: "FK_CaChieu_LoaiHinh_MaLoaiHinh",
                        column: x => x.MaLoaiHinh,
                        principalTable: "LoaiHinh",
                        principalColumn: "MaLoaiHinh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaChieu_Phim_MaPhim",
                        column: x => x.MaPhim,
                        principalTable: "Phim",
                        principalColumn: "MaPhim",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phim_TheLoai",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaPhim = table.Column<long>(type: "bigint", nullable: false),
                    MaTheLoai = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phim_TheLoai", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phim_TheLoai_Phim_MaPhim",
                        column: x => x.MaPhim,
                        principalTable: "Phim",
                        principalColumn: "MaPhim",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phim_TheLoai_TheLoai_MaTheLoai",
                        column: x => x.MaTheLoai,
                        principalTable: "TheLoai",
                        principalColumn: "MaTheLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ghe",
                columns: table => new
                {
                    MaGhe = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hang = table.Column<long>(type: "bigint", nullable: false),
                    Cot = table.Column<long>(type: "bigint", nullable: false),
                    MaLoaiGhe = table.Column<long>(type: "bigint", nullable: false),
                    MaCaChieu = table.Column<long>(type: "bigint", nullable: false),
                    MaHoaDon = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ghe", x => x.MaGhe);
                    table.ForeignKey(
                        name: "FK_Ghe_CaChieu_MaCaChieu",
                        column: x => x.MaCaChieu,
                        principalTable: "CaChieu",
                        principalColumn: "MaCaChieu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ghe_HoaDon_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ghe_LoaiGhe_MaLoaiGhe",
                        column: x => x.MaLoaiGhe,
                        principalTable: "LoaiGhe",
                        principalColumn: "MaLoaiGhe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaChieu_MaLoaiHinh",
                table: "CaChieu",
                column: "MaLoaiHinh");

            migrationBuilder.CreateIndex(
                name: "IX_CaChieu_MaPhim",
                table: "CaChieu",
                column: "MaPhim");

            migrationBuilder.CreateIndex(
                name: "IX_Ghe_Hang_Cot_MaCaChieu",
                table: "Ghe",
                columns: new[] { "Hang", "Cot", "MaCaChieu" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ghe_MaCaChieu",
                table: "Ghe",
                column: "MaCaChieu");

            migrationBuilder.CreateIndex(
                name: "IX_Ghe_MaHoaDon",
                table: "Ghe",
                column: "MaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_Ghe_MaLoaiGhe",
                table: "Ghe",
                column: "MaLoaiGhe");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaNguoiDung",
                table: "HoaDon",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_Phim_TheLoai_MaPhim_MaTheLoai",
                table: "Phim_TheLoai",
                columns: new[] { "MaPhim", "MaTheLoai" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phim_TheLoai_MaTheLoai",
                table: "Phim_TheLoai",
                column: "MaTheLoai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ghe");

            migrationBuilder.DropTable(
                name: "Phim_TheLoai");

            migrationBuilder.DropTable(
                name: "CaChieu");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "LoaiGhe");

            migrationBuilder.DropTable(
                name: "TheLoai");

            migrationBuilder.DropTable(
                name: "LoaiHinh");

            migrationBuilder.DropTable(
                name: "Phim");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
