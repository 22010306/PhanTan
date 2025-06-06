﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "LoaiGhe" (
    "MaLoaiGhe" bigint GENERATED BY DEFAULT AS IDENTITY,
    "TenLoaiGhe" text NOT NULL,
    "GiaTien" numeric(20, 0) NOT NULL,
    CONSTRAINT "PK_LoaiGhe" PRIMARY KEY ("MaLoaiGhe")
);

CREATE TABLE "LoaiHinh" (
    "MaLoaiHinh" bigint GENERATED BY DEFAULT AS IDENTITY,
    "TenLoaiHinh" text NOT NULL,
    CONSTRAINT "PK_LoaiHinh" PRIMARY KEY ("MaLoaiHinh")
);

CREATE TABLE "NguoiDung" (
    "MaNguoiDung" bigint GENERATED BY DEFAULT AS IDENTITY,
    "HoTen" text NOT NULL,
    "Email" text NOT NULL,
    "MatKhauBam" text NOT NULL,
    CONSTRAINT "PK_NguoiDung" PRIMARY KEY ("MaNguoiDung")
);

CREATE TABLE "Phim" (
    "MaPhim" bigint GENERATED BY DEFAULT AS IDENTITY,
    "TenPhim" text NOT NULL,
    "DoDai" bigint NOT NULL,
    "DanhGia" real NOT NULL,
    CONSTRAINT "PK_Phim" PRIMARY KEY ("MaPhim")
);

CREATE TABLE "TheLoai" (
    "MaTheLoai" bigint GENERATED BY DEFAULT AS IDENTITY,
    "TenTheLoai" text NOT NULL,
    CONSTRAINT "PK_TheLoai" PRIMARY KEY ("MaTheLoai")
);

CREATE TABLE "HoaDon" (
    "MaHoaDon" bigint GENERATED BY DEFAULT AS IDENTITY,
    "ThoiGianDat" timestamp with time zone NOT NULL,
    "MaNguoiDung" bigint NOT NULL,
    CONSTRAINT "PK_HoaDon" PRIMARY KEY ("MaHoaDon"),
    CONSTRAINT "FK_HoaDon_NguoiDung_MaNguoiDung" FOREIGN KEY ("MaNguoiDung") REFERENCES "NguoiDung" ("MaNguoiDung") ON DELETE CASCADE
);

CREATE TABLE "CaChieu" (
    "MaCaChieu" bigint GENERATED BY DEFAULT AS IDENTITY,
    "ThoiGianChieu" timestamp with time zone NOT NULL,
    "SoHang" bigint NOT NULL,
    "SoCot" bigint NOT NULL,
    "MaPhim" bigint NOT NULL,
    "MaLoaiHinh" bigint NOT NULL,
    CONSTRAINT "PK_CaChieu" PRIMARY KEY ("MaCaChieu"),
    CONSTRAINT "FK_CaChieu_LoaiHinh_MaLoaiHinh" FOREIGN KEY ("MaLoaiHinh") REFERENCES "LoaiHinh" ("MaLoaiHinh") ON DELETE CASCADE,
    CONSTRAINT "FK_CaChieu_Phim_MaPhim" FOREIGN KEY ("MaPhim") REFERENCES "Phim" ("MaPhim") ON DELETE CASCADE
);

CREATE TABLE "Phim_TheLoai" (
    "Id" bigint GENERATED BY DEFAULT AS IDENTITY,
    "MaPhim" bigint NOT NULL,
    "MaTheLoai" bigint NOT NULL,
    CONSTRAINT "PK_Phim_TheLoai" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Phim_TheLoai_Phim_MaPhim" FOREIGN KEY ("MaPhim") REFERENCES "Phim" ("MaPhim") ON DELETE CASCADE,
    CONSTRAINT "FK_Phim_TheLoai_TheLoai_MaTheLoai" FOREIGN KEY ("MaTheLoai") REFERENCES "TheLoai" ("MaTheLoai") ON DELETE CASCADE
);

CREATE TABLE "Ghe" (
    "MaGhe" bigint GENERATED BY DEFAULT AS IDENTITY,
    "Hang" bigint NOT NULL,
    "Cot" bigint NOT NULL,
    "MaLoaiGhe" bigint NOT NULL,
    "MaCaChieu" bigint NOT NULL,
    "MaHoaDon" bigint NOT NULL,
    CONSTRAINT "PK_Ghe" PRIMARY KEY ("MaGhe"),
    CONSTRAINT "FK_Ghe_CaChieu_MaCaChieu" FOREIGN KEY ("MaCaChieu") REFERENCES "CaChieu" ("MaCaChieu") ON DELETE CASCADE,
    CONSTRAINT "FK_Ghe_HoaDon_MaHoaDon" FOREIGN KEY ("MaHoaDon") REFERENCES "HoaDon" ("MaHoaDon") ON DELETE CASCADE,
    CONSTRAINT "FK_Ghe_LoaiGhe_MaLoaiGhe" FOREIGN KEY ("MaLoaiGhe") REFERENCES "LoaiGhe" ("MaLoaiGhe") ON DELETE CASCADE
);

CREATE INDEX "IX_CaChieu_MaLoaiHinh" ON "CaChieu" ("MaLoaiHinh");

CREATE INDEX "IX_CaChieu_MaPhim" ON "CaChieu" ("MaPhim");

CREATE UNIQUE INDEX "IX_Ghe_Hang_Cot_MaCaChieu" ON "Ghe" ("Hang", "Cot", "MaCaChieu");

CREATE INDEX "IX_Ghe_MaCaChieu" ON "Ghe" ("MaCaChieu");

CREATE INDEX "IX_Ghe_MaHoaDon" ON "Ghe" ("MaHoaDon");

CREATE INDEX "IX_Ghe_MaLoaiGhe" ON "Ghe" ("MaLoaiGhe");

CREATE INDEX "IX_HoaDon_MaNguoiDung" ON "HoaDon" ("MaNguoiDung");

CREATE UNIQUE INDEX "IX_Phim_TheLoai_MaPhim_MaTheLoai" ON "Phim_TheLoai" ("MaPhim", "MaTheLoai");

CREATE INDEX "IX_Phim_TheLoai_MaTheLoai" ON "Phim_TheLoai" ("MaTheLoai");

INSERT INTO
    "__EFMigrationsHistory" (
        "MigrationId",
        "ProductVersion"
    )
VALUES (
        '20250530153642_Init',
        '9.0.5'
    );

COMMIT;