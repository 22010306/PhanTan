using DatabaseModels.Models;

namespace DatabaseModels.Samples;

public class LoaiGheSample
{
  public static List<LoaiGhe> Data { get; } = [
    new LoaiGhe { MaLoaiGhe = 1, TenLoaiGhe = "Ghế Thường", GiaTien = 50000 },
    new LoaiGhe { MaLoaiGhe = 2, TenLoaiGhe = "Ghế VIP", GiaTien = 100000 },
  ];
}