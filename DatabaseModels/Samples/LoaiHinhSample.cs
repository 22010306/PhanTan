using DatabaseModels.Models;

namespace DatabaseModels.Samples;

public static class LoaiHinhSample
{
  public static List<LoaiHinh> Data { get; } = [
    new LoaiHinh { MaLoaiHinh = 1, TenLoaiHinh = "2D" },
    new LoaiHinh { MaLoaiHinh = 2, TenLoaiHinh = "3D" },
    new LoaiHinh { MaLoaiHinh = 3, TenLoaiHinh = "IMAX 2D" },
    new LoaiHinh { MaLoaiHinh = 4, TenLoaiHinh = "IMAX 3D" },
    new LoaiHinh { MaLoaiHinh = 5, TenLoaiHinh = "4DX" },
    new LoaiHinh { MaLoaiHinh = 6, TenLoaiHinh = "ScreenX" },
    new LoaiHinh { MaLoaiHinh = 7, TenLoaiHinh = "Dolby Atmos" },
    new LoaiHinh { MaLoaiHinh = 8, TenLoaiHinh = "Suất chiếu đặc biệt" }
  ];
}